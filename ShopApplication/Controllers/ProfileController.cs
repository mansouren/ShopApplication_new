using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using ShopApplication.Common.Utilities.Common.Utilities;
using ShopApplication.DataLayer.Entities;
using ShopApplication.Models;
using ShopApplication.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;


namespace ShopApplication.Controllers
{

    public class ProfileController : Controller
    {
        private readonly IUserService userService;
        private readonly IShoppingCartService shoppingCartService;

        public ProfileController(IUserService userService, IShoppingCartService shoppingCartService)
        {
            this.userService = userService;
            this.shoppingCartService = shoppingCartService;
        }
        public async Task<IActionResult> Index()
        {

            string mobile = User.Identity.FindFirstValue(ClaimTypes.Name);
            var user = await userService.GetUserByMobile(mobile);
            if (user.IsActive == false)
            {
                return RedirectToAction("Activate", "Account");
            }

            return View();

        }

        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordDto dto, CancellationToken cancellationToken)
        {
            if (ModelState.IsValid)
            {
                var user = await userService.IsExistUser(User.Identity.Name, dto.OldPassword);
                if (user != null)
                {
                    await userService.ChangePassword(dto.Password, user, cancellationToken);
                    if (user.Role.RoleName == "Admin")
                    {
                        return Redirect("/Admin");
                    }
                    else
                    {
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    ModelState.AddModelError("OldPassword", "کلمه عبور جاری اشتباه وارد شده است");
                }
            }

            return View(dto);
        }

        public async Task<IActionResult> ShowShoppingCart()
        {
            User user = await userService.GetUserByMobile(User.Identity.Name);
            Factor factor = await shoppingCartService.GetFactorByUserId(user.Id);
            if (factor != null)
            {
                var factorDetail = await shoppingCartService.GetFactorDetailByFactorId(factor.Id);
                if (factorDetail.Count() > 0)
                {
                    return View(factorDetail);
                }
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }



        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            await shoppingCartService.DeleteFactorDetail(id, cancellationToken);
            return RedirectToAction("ShowShoppingCart");
        }

        public async Task<IActionResult> SelectAddress()
        {
            var user = await userService.GetUserByMobile(User.Identity.Name);
            var addresses = await shoppingCartService.GetAddressByUser(user.Id);
            ViewData["Addresslist"] = new SelectList(addresses, "Id", "AddressText");
            return PartialView(addresses);
        }


        [Route("UpdateAddress/{addressid}")]
        public async Task<IActionResult> UpdateAddress(int? addressid, CancellationToken cancellationToken)
        {
            var user = await userService.GetUserByMobile(User.Identity.Name);
            var factor = await shoppingCartService.GetFactorByUserId(user.Id);
            factor.AddressId = addressid;
            await shoppingCartService.UpdateFactor(factor, cancellationToken);
            return RedirectToAction("ShowFactor");
        }


        public async Task<IActionResult> ShowFactor()
        {
            User user = await userService.GetUserByMobile(User.Identity.Name);
            Factor factor = await shoppingCartService.GetFactorByUserId(user.Id);
            ViewBag.factoraddress = factor.AddressId;
            if (factor != null)
            {
                var factorDetail = await shoppingCartService.GetFactorDetailByFactorId(factor.Id);
                if (factorDetail.Count() > 0)
                {
                    return View(factorDetail);
                }
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
