using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SASTE.Data;
using SASTE.Models;
using SASTE.Data;
using SASTE.Models;

namespace SASTE.Pages.Warehouse
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Product> Products { get; set; } = new List<Product>();

        public async Task OnGetAsync()
        {
            Products = await _context.Products
                                     .OrderBy(p => p.StockOnHand)
                                     .ToListAsync();
        }
    }
}