using System.ComponentModel.DataAnnotations;

namespace KYC360.Services.Models.Dto;

public class SecretUrlDto
{
    [StringLength(50, ErrorMessage = "UserName must be at most 50 characters")]
    [RegularExpression(@"^[a-zA-Z0-9]*$", ErrorMessage = "UserName can only contain letters and numbers")]
    public required string UserName { get; set; }
    public int ClickTimes { get; set; } = 1;
    public DateTimeOffset? ValidTo { get; set; }
}
