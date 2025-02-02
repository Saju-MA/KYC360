namespace KYC360.Web.ViewModels.SecretUrlViewModels;

public class SupersecretViewModel(string? result)
{
    public string Text { get; } = string.IsNullOrEmpty(result) ? "There are no secrets here." : result;
}
