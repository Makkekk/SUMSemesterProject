using System.Net.Http.Json;
using DTO;

namespace LajmiBlazorWebApp.Client.Services;

public class DiscountAgreementService
{
    private readonly HttpClient _http;

    public DiscountAgreementService(HttpClient http)
    {
        _http = http;
    }

    public async Task<List<DiscountAgreementDto>> GetAllDiscountsAsync()
    {
        try
        {
            var d = await _http.GetFromJsonAsync<List<DiscountAgreementDto>>("api/discount");
            return d ?? new List<DiscountAgreementDto>();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching discount: {ex.Message}");
            return new List<DiscountAgreementDto>();
        }
    }
}