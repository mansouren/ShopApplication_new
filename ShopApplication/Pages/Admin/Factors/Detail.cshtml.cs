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
    public class DetailModel : PageModel
    {
        private readonly IRepository<FactorDetail> repository;

        public DetailModel(IRepository<FactorDetail> repository)
        {
            this.repository = repository;
        }

        [BindProperty]
        public IEnumerable<FactorDetail> FactorDetails { get; set; }
        public void OnGet(int id)
        {
            FactorDetails = repository.TableAsNoTracking.Include(f=> f.Product)
                               .Where(f => f.FactorId == id).ToList();
        }
    }
}
