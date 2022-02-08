using Microsoft.AspNetCore.Mvc;
using ShopApplication.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ShopApplication.Services;
using BankMellat;
using System.Globalization;
using ShopApplication.DataLayer.Repositories.Contracts;
using ShopApplication.DataLayer.Entities;
using System.Net.Http;

namespace ShopApplication.Controllers
{
    public class PaymentController : Controller
    {
        private readonly IShoppingCartService shoppingCartService;
        private readonly IRepository<Settings> settingRepo;
        private readonly IHttpClientFactory httpClientFactory;
        PersianCalendar pc = new PersianCalendar();
        public PaymentController(IShoppingCartService shoppingCartService
            ,IRepository<Settings> settingRepo, IHttpClientFactory httpClientFactory)
        {
            this.shoppingCartService = shoppingCartService;
            this.settingRepo = settingRepo;
            this.httpClientFactory = httpClientFactory;
        }
        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {
            if (User.Identity.IsAuthenticated)
            {
                var user =await shoppingCartService.GetUserbyMobile(User.Identity.Name);
                var factor =await shoppingCartService.GetFactorByUserId(user.Id);
                var factorDetails =await shoppingCartService.GetFactorDetailByFactorId(factor.Id);
                double sumFactor = 0;
                foreach (var item in factorDetails)
                {
                    var product =await shoppingCartService.GetProductById(item.ProductId);
                    item.UnitPrice = product.Price; //Maybe factor has opened for a while and product's price has been changed
                    await shoppingCartService.UpdateFactorDetail(item, cancellationToken);
                    double sum = item.UnitPrice * item.ProductCount;
                    sumFactor += sum;
                }
                factor.Price = Convert.ToInt32(sumFactor);
                await shoppingCartService.UpdateFactor(factor, cancellationToken);

                long orderID = Convert.ToInt64(factor.Number);
                long priceAmount = Convert.ToInt64(sumFactor);
                string additionalText = "خرید جدید در فروشگاه"; // توضیحات شما برای این تراکنش

                Services.BankMellat bankmellat = new Services.BankMellat();

                bpPayRequestResponse resultRequest =await bankmellat.bpPayRequest(orderID, priceAmount, additionalText);
                string[] StatusSendRequest = resultRequest.ToString().Split(',');
                if (int.Parse(StatusSendRequest[0]) == (int)Services.BankMellat.MellatBankReturnCode.ﺗﺮاﻛﻨﺶ_ﺑﺎ_ﻣﻮﻓﻘﻴﺖ_اﻧﺠﺎم_ﺷﺪ)
                {
                    return RedirectToAction("RedirectVPOS", "Payment", new { id = StatusSendRequest[1] });
                }
                TempData["Message"] = bankmellat.DesribtionStatusCode(int.Parse(StatusSendRequest[0].Replace("_", " ")));
                return RedirectToAction("ShowError", "Payment");
            }
            return RedirectToAction("Login", "Account");
        }

        public ActionResult RedirectVPOS(string id)
        {
            try
            {
                if (id == null)
                {
                    TempData["Message"] = "هیچ شماره پیگیری برای پرداخت از سمت بانک ارسال نشده است!";

                    return RedirectToAction("ShowError", "Payment");
                }
                else
                {
                    ViewBag.id = id;
                    return View();
                }
            }
            catch (Exception error)
            {
                TempData["Message"] = error + "متاسفانه خطایی رخ داده است، لطفا مجددا عملیات خود را انجام دهید در صورت تکرار این مشکل را به بخش پشتیبانی اطلاع دهید";

                return RedirectToAction("ShowError", "Payment");
            }
        }

        public ActionResult ShowError()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> BankCallback(CancellationToken cancellationToken)
        {
            bool Run_bpReversalRequest = false;
            long saleReferenceId = -999;
            long saleOrderId = -999;
            string resultCode_bpPayRequest;

            Services.BankMellat bankmellat = new Services.BankMellat();

            try
            {
                saleReferenceId = long.Parse(Request.Query["SaleReferenceId"].ToString());
                saleOrderId = long.Parse(Request.Query["SaleOrderId"].ToString());
                resultCode_bpPayRequest = Request.Query["ResCode"].ToString();

                //Result Code
                string resultCode_bpinquiryRequest = "-9999";
                string resultCode_bpSettleRequest = "-9999";
                string resultCode_bpVerifyRequest = "-9999";

                if (int.Parse(resultCode_bpPayRequest) == (int)Services.BankMellat.MellatBankReturnCode.ﺗﺮاﻛﻨﺶ_ﺑﺎ_ﻣﻮﻓﻘﻴﺖ_اﻧﺠﺎم_ﺷﺪ)
                {
                    resultCode_bpVerifyRequest =(await bankmellat.VerifyRequest(saleOrderId, saleOrderId, saleReferenceId)).ToString();

                    if (string.IsNullOrEmpty(resultCode_bpVerifyRequest))
                    {
                        resultCode_bpinquiryRequest =(await bankmellat.InquiryRequest(saleOrderId, saleOrderId, saleReferenceId)).ToString();
                        if (int.Parse(resultCode_bpinquiryRequest) != (int)Services.BankMellat.MellatBankReturnCode.ﺗﺮاﻛﻨﺶ_ﺑﺎ_ﻣﻮﻓﻘﻴﺖ_اﻧﺠﺎم_ﺷﺪ)
                        {
                            //the transactrion faild
                            TempData["Message"] = bankmellat.DesribtionStatusCode(int.Parse(resultCode_bpinquiryRequest.Replace("_", " ")));
                            Run_bpReversalRequest = true;
                        }
                    }

                    if ((int.Parse(resultCode_bpVerifyRequest) == (int)Services.BankMellat.MellatBankReturnCode.ﺗﺮاﻛﻨﺶ_ﺑﺎ_ﻣﻮﻓﻘﻴﺖ_اﻧﺠﺎم_ﺷﺪ)
                        ||
                        (int.Parse(resultCode_bpinquiryRequest) == (int)Services.BankMellat.MellatBankReturnCode.ﺗﺮاﻛﻨﺶ_ﺑﺎ_ﻣﻮﻓﻘﻴﺖ_اﻧﺠﺎم_ﺷﺪ))
                    {
                        resultCode_bpSettleRequest =(await bankmellat.SettleRequest(saleOrderId, saleOrderId, saleReferenceId)).ToString();
                        if ((int.Parse(resultCode_bpSettleRequest) == (int)Services.BankMellat.MellatBankReturnCode.ﺗﺮاﻛﻨﺶ_ﺑﺎ_ﻣﻮﻓﻘﻴﺖ_اﻧﺠﺎم_ﺷﺪ)
                            || (int.Parse(resultCode_bpSettleRequest) == (int)Services.BankMellat.MellatBankReturnCode.ﺗﺮاﻛﻨﺶ_Settle_ﺷﺪه_اﺳﺖ))
                        {
                            TempData["Message"] = "تراکنش شما با موفقیت انجام شد ";
                            TempData["Message"] += Environment.NewLine + " لطفا شماره پیگیری را یادداشت نمایید" + Environment.NewLine + saleReferenceId;
                        }
                        else
                        {
                            TempData["Message"] = bankmellat.DesribtionStatusCode(int.Parse(resultCode_bpSettleRequest.Replace("_", " ")));
                            Run_bpReversalRequest = true;
                        }

                        string strToday = pc.GetYear(DateTime.Now).ToString("0000") + "/" + pc.GetMonth(DateTime.Now).ToString("00") + "/" + pc.GetDayOfMonth(DateTime.Now).ToString("00");

                        var factor =await shoppingCartService.GetFactorByFactorNumber(saleOrderId.ToString());

                        factor.IsPayed = true;
                        factor.PayNumber = saleReferenceId.ToString();
                        factor.PayDate =DateTime.Parse(strToday);
                        factor.PayTime = DateTime.Parse(DateTime.Now.ToShortTimeString());

                        await shoppingCartService.UpdateFactor(factor, cancellationToken);

                        var factorDetails =await shoppingCartService.GetFactorDetailByFactorId(factor.Id);
                        foreach (var item in factorDetails)
                        {
                            var product =await shoppingCartService.GetProductById(item.ProductId);
                            product.Qty -= item.ProductCount;
                            await shoppingCartService.UpdateProduct(product, cancellationToken);
                        }
                        var user =await shoppingCartService.GetUserbyMobile(User.Identity.Name);

                        var setting = settingRepo.TableAsNoTracking.FirstOrDefault();

                        SendSms sms = new SendSms(httpClientFactory);
                        try
                        {
                            if (setting.FactorIsSend)
                            {
                               await sms.SendMessagess(user.Mobile, "صورت حساب جدید شما در فروشگاه با مبلغ " + factor.Price.ToString());
                            }

                            if (setting.PayIsSend)
                            {
                               await sms.SendMessagess(user.Mobile, "صورت حساب جدید شما در فروشگاه با موفقیت پرداخت شد. شماره پیگیری شما  " + factor.Number.ToString());
                            }
                        }
                        catch
                        {

                        }
                    }
                    else
                    {
                        TempData["Message"] = bankmellat.DesribtionStatusCode(int.Parse(resultCode_bpVerifyRequest.Replace("_", " ")));
                        Run_bpReversalRequest = true;
                    }
                }
                else
                {
                    TempData["Message"] = bankmellat.DesribtionStatusCode(int.Parse(resultCode_bpPayRequest)).Replace("_", " ");
                    Run_bpReversalRequest = true;
                }

                return RedirectToAction("ShowError", "Payment");
            }
            catch (Exception Error)
            {
                TempData["Message"] = "متاسفانه خطایی رخ داده است، لطفا مجددا عملیات خود را انجام دهید در صورت تکرار این مشکل را به بخش پشتیبانی اطلاع دهید";
                // Save and send Error for admin user
                Run_bpReversalRequest = true;
                return RedirectToAction("ShowError", "Payment");
            }
            finally
            {
                if (Run_bpReversalRequest) //ReversalRequest
                {
                    if (saleOrderId != -999 && saleReferenceId != -999)
                       await bankmellat.bpReversalRequest(saleOrderId, saleOrderId, saleReferenceId);
                    // Save information to Database...
                }
            }
        }
    }
}
