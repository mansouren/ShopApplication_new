using FluentValidation;
using ShopApplication.DataLayer.Entities;
using ShopApplication.WebFrameWorks.Core;


namespace ShopApplication.Models
{
    public class AddressDto : BaseDTO<AddressDto, Address>
    {
        public string AddressText { get; set; }
        public string PostalCode { get; set; }
        public int StateId { get; set; }
        public string CityId { get; set; }
       
    }

    public class AddressDtoValidation : AbstractValidator<AddressDto>
    {
        public AddressDtoValidation()
        {
            RuleFor(a => a.AddressText).NotEmpty().WithMessage("لطفا مقداری را وارد نمایید");

            RuleFor(a => a.StateId).NotEmpty().WithMessage("لطفا مقداری را انتخاب نمایید");
            
            RuleFor(a => a.CityId).NotEmpty().WithMessage("لطفا مقداری را انتخاب نمایید");
            
            RuleFor(a => a.PostalCode).NotEmpty().WithMessage("لطفا مقداری را وارد نمایید")
                .MaximumLength(10).WithMessage("کد پستی نمی تواند بیشتر از 10 کاراکتر باشد")
                .MinimumLength(10).WithMessage("کد پستی نمی تواند بیشتر از 10 کاراکتر باشد");
        }
    }
}
