using System.ComponentModel.DataAnnotations;

namespace SASTE.Models;

public class RoleChangeAudit
{
    public long Id { get; set; }

    [Required] public string TargetUserId { get; set; } = "";
    [Required] public string TargetUserEmail { get; set; } = "";

    public string? OldRole { get; set; }
    public string? NewRole { get; set; }

    [Required] public string ChangedByUserId { get; set; } = "";
    [Required] public string ChangedByEmail { get; set; } = "";

    public DateTime ChangedAtUtc { get; set; } = DateTime.UtcNow;

    [MaxLength(500)]
    public string? Note { get; set; }
}
