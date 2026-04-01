using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SASTE.Models;

public class MonthlyGoalGlobal
{
    public int Id { get; set; }

    [Range(2000, 3000)]
    public int Year { get; set; }

    [Range(1, 12)]
    public int Month { get; set; }

    [Precision(18, 2)]
    public decimal TargetAmount { get; set; }

    public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
}
