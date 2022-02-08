using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ShopApplication.DataLayer.Entities;
using ShopApplication.DataLayer.Repositories.Contracts;
using ShopApplication.Models;

namespace ShopApplication.Pages.Admin.Products
{
    public class IndexModel : PageModel
    {
        private readonly IProductRepository repository;
       
        public IndexModel(IProductRepository repository)
        {
            this.repository = repository;
           
        }

        [BindProperty]
        public IEnumerable<Product> products { get; set; }
       
        public void OnGet(string strsearch)
        {
            products = repository.Table.Include(p => p.Brand).Include(p => p.Group).ToList();
            if (!string.IsNullOrEmpty(strsearch))
            {
                products = repository.TableAsNoTracking.Include(p => p.Brand).Include(p => p.Group)
                          .Where(p => p.Name.Contains(strsearch)).ToList();
            }
        }
    }
}
