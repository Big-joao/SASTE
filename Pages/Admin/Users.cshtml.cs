using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SASTE.Pages.Admin
{
    [Authorize(Roles = "Admin")]
    public class UsersModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UsersModel(
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public List<UserViewModel> Users { get; set; } = new();
        public List<string> Roles { get; set; } = new();

        public class UserViewModel
        {
            public string Id { get; set; } = "";
            public string Email { get; set; } = "";
            public string CurrentRole { get; set; } = "Cliente";
        }

        public async Task OnGetAsync()
        {
            Roles = _roleManager.Roles
                .Select(r => r.Name!)
                .Where(r => r != "Admin")
                .ToList();

            foreach (var user in _userManager.Users.ToList())
            {
                var roles = await _userManager.GetRolesAsync(user);

                Users.Add(new UserViewModel
                {
                    Id = user.Id,
                    Email = user.Email ?? "",
                    CurrentRole = roles.FirstOrDefault() ?? "Cliente"
                });
            }
        }

        public async Task<IActionResult> OnPostSetRoleAsync(string userId, string selectedRole)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return RedirectToPage();

            var currentRoles = await _userManager.GetRolesAsync(user);

            if (currentRoles.Any())
                await _userManager.RemoveFromRolesAsync(user, currentRoles);

            if (!string.IsNullOrWhiteSpace(selectedRole) && await _roleManager.RoleExistsAsync(selectedRole))
                await _userManager.AddToRoleAsync(user, selectedRole);

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDeleteUserAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return RedirectToPage();

            await _userManager.DeleteAsync(user);
            return RedirectToPage();
        }
    }
}