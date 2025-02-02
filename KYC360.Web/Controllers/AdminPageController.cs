using KYC360.Web.Models;
using KYC360.Web.Services;
using KYC360.Web.ViewModels.AdminPageViewModels;
using Microsoft.AspNetCore.Mvc;

namespace KYC360.Web.Controllers;

public class AdminPageController(ISecretUrlService secretUrlService) : Controller
{
    private readonly ISecretUrlService _secretUrlService = secretUrlService;

    [HttpGet]
    public IActionResult Index()
    {
        return View(new AdminPageViewModel());
    }

    [HttpPost]
    public async Task<IActionResult> Index(AdminPageViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View("Index", model);
        }        
        try
        {
            model.Minutes = model.Minutes == 0 ? null : model.Minutes;
            var response = await _secretUrlService.CreateUrlAsync(new SecretUrlDto
            {
                UserName = model.UserName,
                ClickTimes = model.ClickTimes,
                ValidTo = model.Minutes.HasValue ? DateTime.UtcNow.AddMinutes(model.Minutes.Value) : null
            });

            if (response!.IsSuccess)
            {
                model.GeneratedUrl = $"{Request.Scheme}://{Request.Host}/supersecret?id={response.Result}";
                model.CanCopy = true;
            }
            else
            {
                model.GeneratedUrl = response.Message;
            }
            return View(model);
        }
        catch (Exception)
        {
            ModelState.AddModelError("", "An error occurred while generating the URL.");
            return View(model);
        }
    }
}
