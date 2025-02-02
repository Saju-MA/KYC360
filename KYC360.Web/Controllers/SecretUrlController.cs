using KYC360.Web.Services;
using KYC360.Web.ViewModels.SecretUrlViewModels;
using Microsoft.AspNetCore.Mvc;

namespace KYC360.Web.Controllers;

[Route("/supersecret")]
public class SecretUrlController(ISecretUrlService secretUrlService) : Controller
{
    private readonly ISecretUrlService _secretUrlService = secretUrlService;

    [HttpGet]
    public async Task<IActionResult> Index([FromQuery] Guid id)
    {        
        try
        {
            var response = await _secretUrlService.GetUrlAsync(id);

            if (response == null || !response.IsSuccess)
            {
                return View(new SupersecretViewModel(null));
            }
            return View(new SupersecretViewModel(response.Result?.ToString()));
        }
        catch (Exception)
        {
            return View(new SupersecretViewModel(null));
        }
    }
}
