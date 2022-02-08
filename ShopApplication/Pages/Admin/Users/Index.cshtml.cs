using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ShopApplication.DataLayer.Entities;
using ShopApplication.DataLayer.Repositories;
using ShopApplication.DataLayer.Repositories.Contracts;

namespace ShopApplication.Pages.Admin.Users
{
    //[AllowAnonymous]
    public class IndexModel : PageModel
    {
        private readonly IUserRepository repository;

        public IndexModel(IUserRepository repository)
        {
            this.repository = repository;
        }
        [BindProperty]
        public List<User> users { get; set; }
        
        public void OnGet(string strsearch)
        {
            users = repository.TableAsNoTracking.Include(u => u.Role).ToList();
            if (!string.IsNullOrEmpty(strsearch))
            {
                users = users.Where(u => u.Mobile.Contains(strsearch)).ToList();
            }
        }

       
    }
}
