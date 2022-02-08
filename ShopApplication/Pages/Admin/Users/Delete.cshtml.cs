using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopApplication.DataLayer.Entities;
using ShopApplication.DataLayer.Repositories;

namespace ShopApplication.Pages.Admin.Users
{
    public class DeleteModel : PageModel
    {
        private readonly IUserRepository userRepository;

        public DeleteModel(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }
        [BindProperty]
        public User User { get; set; }
        public async void OnGet(int id,CancellationToken cancellationToken)
        {
            User =await userRepository.GetByIdAsync(cancellationToken,id);
        }

        public async Task<IActionResult> OnPost(int id,CancellationToken cancellationToken)
        {
            await userRepository.DeleteAsync(User, cancellationToken);
            return RedirectToPage("Index");
        }
    }
}
