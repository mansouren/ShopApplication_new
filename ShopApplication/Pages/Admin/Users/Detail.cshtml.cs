using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ShopApplication.DataLayer.Entities;
using ShopApplication.DataLayer.Repositories.Contracts;

namespace ShopApplication.Pages.Admin.Users
{
    public class DetailModel : PageModel
    {
        private readonly IRepository<Address> repository;

        public DetailModel(IRepository<Address> repository)
        {
            this.repository = repository;
        }
        public IEnumerable<Address> Addresses { get; set; }
        public void OnGet(int? id)
        {
            Addresses = repository.TableAsNoTracking.Include(a => a.City).ThenInclude(c => c.State)
                        .Where(a => a.UserId == id).ToList();
        }
    }
}
