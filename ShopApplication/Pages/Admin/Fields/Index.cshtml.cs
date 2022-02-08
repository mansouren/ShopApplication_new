using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopApplication.DataLayer.Entities;
using ShopApplication.DataLayer.Repositories.Contracts;

namespace ShopApplication.Pages.Admin.Fields
{
    public class IndexModel : PageModel
    {
        private readonly IRepository<Feild> repository;

        public IndexModel(IRepository<Feild> repository)
        {
            this.repository = repository;
        }
        [BindProperty]
        public IEnumerable<Feild> Feildlst { get; set; }
        public void OnGet()
        {
            Feildlst = repository.TableAsNoTracking.ToList();
        }
    }
}
