using Microsoft.AspNetCore.Mvc;
using KYC360.Services.Services;
using KYC360.Services.Models.Dto;

namespace KYC360.Services.Controllers;

[Route("api/SecretUrlAPI")]
[ApiController]
public class SecretUrlAPIController(ISecretUrlService secretUrlService) : ControllerBase
{
    private readonly ISecretUrlService _secretUrlService = secretUrlService;

    [HttpGet]
    public async Task<IActionResult> GetUrl([FromQuery] Guid id)
    {
        var response = await _secretUrlService.GetUrlAsync(id);
        if(response.IsSuccess)
            return Ok(response);
        else
            return NotFound(response);
    }

    [HttpPost]
    public async Task<IActionResult> CreateUrl([FromBody] SecretUrlDto secretUrl)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var response = await _secretUrlService.CreateUrlAsync(secretUrl);
        if (!response.IsSuccess)
        {
            return StatusCode(500, response);
        }
        return CreatedAtAction(nameof(GetUrl), new { id = response.Result }, response);
    }    
}
