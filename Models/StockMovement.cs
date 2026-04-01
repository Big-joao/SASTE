using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SASTE.Models;

public enum StockMovementType
{
    In = 1,     // Entrada
    Out = 2,    // Saída
    Adjust = 3  // Ajuste
}

public class StockMovement
{
    public long Id { get; set; }

    [Required]
    public int ProductId { get; set; }
    public Product Product { get; set; } = null!;

    public StockMovementType Type { get; set; }

    // Quantidade sempre positiva; o Type indica se entra/sai
    [Range(1, int.MaxValue)]
    public int Quantity { get; set; }

    [MaxLength(500)]
    public string? Note { get; set; }

    // Quem registou (Identity user)
    [Required]
    public string CreatedByUserId { get; set; } = string.Empty;

    public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
}
