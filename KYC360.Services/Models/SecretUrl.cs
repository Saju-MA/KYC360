using System.Diagnostics.CodeAnalysis;
using System.ComponentModel.DataAnnotations;

namespace KYC360.Services.Models;

public class SecretUrl
{
    [Key]
    public Guid Id { get; set; }
    [Required]
    [StringLength(50)]
    public required string UserName { get; set; }
    [Required]
    public int ClickTimes { get; set; }
    [AllowNull]
    public DateTimeOffset? ValidTo { get; set; }
}
