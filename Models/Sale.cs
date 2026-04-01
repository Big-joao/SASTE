using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SASTE.Models;

public enum SaleStatus
{
    Draft = 1,
    Completed = 2,
    Cancelled = 3
}

public class Sale
{
    public long Id { get; set; }

    [Required, MaxLength(50)]
    public string SaleNumber { get; set; } = string.Empty; // ex: V2026-0001

    public bool HasWarranty { get; set; } = false;   // garantia sim/não
    public string? CustomerUserId { get; set; }  // cliente associado (opcional)

    public int? Installments { get; set; }           // prestações (null = pagamento normal)


    public SaleStatus Status { get; set; } = SaleStatus.Completed;

    // Quem fez a venda (Caixa/Vendedor)
    [Required]
    public string SoldByUserId { get; set; } = string.Empty;

    [Precision(18, 2)]
    public decimal Subtotal { get; set; }

    [Precision(18, 2)]
    public decimal Discount { get; set; }

    [Precision(18, 2)]
    public decimal Total { get; set; }

    public DateTime SoldAtUtc { get; set; } = DateTime.UtcNow;

    public ICollection<SaleItem> Items { get; set; } = new List<SaleItem>();
}
