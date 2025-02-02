using KYC360.Web.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using static KYC360.Web.Utility.Utility;

namespace KYC360.Web.Services;

public interface IBaseService
{
    public Task<ResponseDto?> SendAsync(RequestDto requestDto);
}
public class BaseService(IHttpClientFactory httpClientFactory) : IBaseService
{
    private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;
    public async Task<ResponseDto?> SendAsync(RequestDto requestDto)
    {
        try
        {
            using HttpClient client = _httpClientFactory.CreateClient("KYC360API");
            using HttpRequestMessage message = new()
            {
                Method = requestDto.ApiType switch
                {
                    ApiType.GET => HttpMethod.Get,
                    ApiType.POST => HttpMethod.Post,
                    ApiType.PUT => HttpMethod.Put,
                    ApiType.DELETE => HttpMethod.Delete,
                    _ => HttpMethod.Get,
                },
                RequestUri = new Uri(requestDto.Url)
            };

            message.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            if (requestDto.Data != null)
            {
                message.Content = new StringContent(JsonConvert.SerializeObject(requestDto.Data), Encoding.UTF8, "application/json");
            }

            HttpResponseMessage apiResponse = await client.SendAsync(message);

            if (!apiResponse.IsSuccessStatusCode)
            {
                return new ResponseDto
                {
                    Message = apiResponse.StatusCode switch
                    {
                        System.Net.HttpStatusCode.NotFound => "Not Found",
                        System.Net.HttpStatusCode.Forbidden => "Access Denied",
                        System.Net.HttpStatusCode.Unauthorized => "Unauthorized",
                        System.Net.HttpStatusCode.InternalServerError => "Internal Server Error",
                        System.Net.HttpStatusCode.BadRequest => "Bad Request",
                        _ => "Request Failed"
                    }
                };
            }

            var apiContent = await apiResponse.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ResponseDto>(apiContent) ?? new ResponseDto { IsSuccess = false, Message = "Invalid API response" };
        }
        catch (Exception)
        {
            return new ResponseDto
            {
                Message = "An error occurred while processing the request."
            };
        }
    }
}
