using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopApplication.DataLayer.Entities;
using ShopApplication.DataLayer.Repositories.Contracts;

namespace ShopApplication.Pages.Admin.Factors
{
    public class IndexModel : PageModel
    {
        private readonly IRepository<Factor> repository;

        public IndexModel(IRepository<Factor> repository)
        {
            this.repository = repository;
        }

        [BindProperty]
        public IEnumerable<Factor> Factors { get; set; }
        public void OnGet()
        {
            Factors = repository.TableAsNoTracking.Where(f => f.IsPayed == true).OrderByDescending(f => f.PayDate).ToList();
            
        }
    }
}
