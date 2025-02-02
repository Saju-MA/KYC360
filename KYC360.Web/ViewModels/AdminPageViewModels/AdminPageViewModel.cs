using System.ComponentModel.DataAnnotations;

namespace KYC360.Web.ViewModels.AdminPageViewModels;

public class AdminPageViewModel
{
    [Required]
    [MaxLength(50, ErrorMessage = "User Name cannot exceed 50 characters.")]
    [RegularExpression(@"^[a-zA-Z0-9]+$", ErrorMessage = "User Name can only contain letters and numbers.")]
    public string UserName { get; set; } = "";

    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Click Times must be greater than 0.")]
    public int ClickTimes { get; set; } = 1;

    public int? Minutes { get; set; }

    public string? GeneratedUrl { get; set; }

    public bool CanCopy { get; set; } = false;
}
