using KYC360.Web.Models;
using static KYC360.Web.Utility.Utility;

namespace KYC360.Web.Services;

public interface ISecretUrlService
{
    public Task<ResponseDto?> GetUrlAsync(Guid id);
    public Task<ResponseDto?> CreateUrlAsync(SecretUrlDto secretUrl);
}
public class SecretUrlService(IBaseService baseService) : ISecretUrlService
{
    private readonly IBaseService _baseService = baseService;

    public async Task<ResponseDto?> GetUrlAsync(Guid id)
    {
        return await _baseService.SendAsync(new RequestDto()
        {
            ApiType = ApiType.GET,
            Url = $"{ServicesAPIBase}/api/SecretUrlAPI?id={id}"
        });
    }
    public async Task<ResponseDto?> CreateUrlAsync(SecretUrlDto secretUrl)
    {
        return await _baseService.SendAsync(new RequestDto()
        {
            ApiType = ApiType.POST,
            Data = secretUrl,
            Url = $"{ServicesAPIBase}/api/SecretUrlAPI"
        });
    }
}
