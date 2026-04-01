using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace SASTE.Models;

public class Product
{
    public int Id { get; set; }

    [Required, MaxLength(50)]
    public string Sku { get; set; } = string.Empty;

    [Required, MaxLength(200)]
    public string Name { get; set; } = string.Empty;

    [MaxLength(1000)]
    public string? Description { get; set; }

    [Precision(18, 2)]
    public decimal UnitPrice { get; set; }

    public bool IsActive { get; set; } = true;

    public decimal Price { get; set; } // <--- É isto que o SQL está a procurar
    public int Stock { get; set; }

    // Stock atual (podes calcular também por movimentos, mas para PAP fica ótimo assim)
    public int StockOnHand { get; set; } = 0;

    public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;

    public ICollection<StockMovement> StockMovements { get; set; } = new List<StockMovement>();
    public ICollection<SaleItem> SaleItems { get; set; } = new List<SaleItem>();
}
