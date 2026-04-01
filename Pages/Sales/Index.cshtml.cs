using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

using SASTE.Data;
using SASTE.Models;
using SASTE.Data;

namespace SASTE.Pages.Sales
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public class SaleDisplayInfo
        {
            public string SaleNumber { get; set; } = string.Empty;
            public DateTime Date { get; set; }
            public string SellerName { get; set; } = string.Empty;
            public string CustomerName { get; set; } = string.Empty;
            public decimal Total { get; set; }
            public string ProductsSummary { get; set; } = string.Empty;
        }

        public IList<SaleDisplayInfo> Sales { get; set; } = new List<SaleDisplayInfo>();

        public async Task OnGetAsync()
        {
            var salesData = await _context.Sales
                .Include(s => s.Items)
                    .ThenInclude(i => i.Product)
                .OrderByDescending(s => s.SoldAtUtc)
                .ToListAsync();

            var users = await _context.Users.ToDictionaryAsync(u => u.Id, u => u.UserName);

            foreach (var sale in salesData)
            {
                var productNames = sale.Items.Select(i => $"{i.Quantity}x {i.Product?.Name}");
                var summary = string.Join(", ", productNames);

                Sales.Add(new SaleDisplayInfo
                {
                    SaleNumber = sale.SaleNumber,
                    Date = sale.SoldAtUtc.ToLocalTime(),
                    Total = sale.Total,
                    ProductsSummary = string.IsNullOrEmpty(summary) ? "Sem produtos" : summary,

                    SellerName = users.ContainsKey(sale.SoldByUserId)
                        ? users[sale.SoldByUserId]
                        : "Desconhecido",

                    CustomerName = !string.IsNullOrEmpty(sale.CustomerUserId) && users.ContainsKey(sale.CustomerUserId)
                        ? users[sale.CustomerUserId]
                        : "Cliente Balcão"
                });
            }
        }
    }
}