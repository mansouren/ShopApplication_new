using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ShopApplication.DataLayer.Entities;
using ShopApplication.DataLayer.Repositories.Contracts;

namespace ShopApplication.Pages.Admin.Factors
{
    public class ShowAddressModel : PageModel
    {
        private readonly IRepository<Address> repository;

        public ShowAddressModel(IRepository<Address> repository)
        {
            this.repository = repository;
        }
        [BindProperty]
        public Address Address { get; set; }
        public void OnGet(int addressid)
        {
            Address = repository.TableAsNoTracking.Include(a => a.City).ThenInclude(c => c.State)
                .FirstOrDefault(a => a.Id == addressid);
        }
    }
}
