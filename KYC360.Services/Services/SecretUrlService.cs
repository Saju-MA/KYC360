using AutoMapper;
using KYC360.Services.Data;
using KYC360.Services.Models;
using KYC360.Services.Models.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace KYC360.Services.Services;

public interface ISecretUrlService
{
    public Task<ResponseDto> GetUrlAsync(Guid id);
    public Task<ResponseDto> CreateUrlAsync(SecretUrlDto secretUrl);
}
public class SecretUrlService(AppDbContext context, IMapper mapper) : ISecretUrlService
{
    private readonly AppDbContext _context = context;
    private readonly IMapper _mapper = mapper;

    public async Task<ResponseDto> GetUrlAsync([FromQuery] Guid id)
    {
        var response = new ResponseDto();
        try
        {
            var url = await _context.SecretUrl.FirstOrDefaultAsync(s => s.Id == id);
            if (url == null)
            {
                response.Message = "Secret URL not found.";
                return response;
            }
            if (url.ClickTimes == 0 || (url.ValidTo != null && url.ValidTo < DateTime.UtcNow))
            {
                response.Message = "Secret URL expired.";
                return response;
            }
            url.ClickTimes--;
            await _context.SaveChangesAsync();

            response.Result = $"You have found the secret, {url.UserName}!";
            response.IsSuccess = true;
        }
        catch (Exception ex)
        {
            response.Message = "An error occurred while processing the request.";
            Console.WriteLine($"Error: {ex.Message}");
        }
        return response;
    }

    public async Task<ResponseDto> CreateUrlAsync(SecretUrlDto secretUrl)
    {
        var response = new ResponseDto();
        try
        {
            SecretUrl obj = _mapper.Map<SecretUrl>(secretUrl);
            response = await CreateSecretUrlWithRetryAsync(obj);
        }
        catch (AutoMapperMappingException)
        {
            response.Message = "Invalid data provided. Please check your input.";
        }
        catch (Exception ex)
        {
            response.Message = "An error occurred. Please try again later.";
            Console.WriteLine($"Error: {ex.Message}");
        }
        return response;
    }

    private async Task<ResponseDto> CreateSecretUrlWithRetryAsync(SecretUrl secretUrl)
    {
        var response = new ResponseDto();
        int retryCount = 0;
        const int maxRetries = 3;

        while (retryCount < maxRetries)
        {
            try
            {
                _context.SecretUrl.Add(secretUrl);
                await _context.SaveChangesAsync();

                response.Result = secretUrl.Id;
                response.IsSuccess = true;
                return response;
            }
            catch (DbUpdateException ex)
            {
                if (IsUniqueConstraintViolation(ex))
                {
                    secretUrl.Id = Guid.NewGuid();
                    retryCount++;
                }
                else
                {
                    response.Message = "An error occurred. Please try again later.";
                    Console.WriteLine($"Error: {ex.Message}");
                    return response;
                }
            }
        }
        response.Message = "Failed to create Secret URL. Please try again later.";
        return response;
    }

    private static bool IsUniqueConstraintViolation(DbUpdateException ex)
    {
        return ex.InnerException is SqlException sqlException && sqlException.Number == 2627;
    }
}
