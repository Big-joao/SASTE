using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Identity; // Necessário para o IdentityUser
using System.Collections.Generic;    // Necessário para o IEnumerable

namespace SASTE.Pages.Admin
{
    public class CustomersModel : PageModel
    {
        // Adiciona a propriedade que o HTML está à procura
        public IEnumerable<IdentityUser> Customers { get; set; } = new List<IdentityUser>();

        public void OnGet()
        {
            // Mais tarde vais preencher esta lista com os dados da base de dados, por exemplo:
            // Customers = _context.Users.ToList();
        }
    }
}