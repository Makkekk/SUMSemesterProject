using System.Net.Http.Json;
using DTO;

namespace LajmiBlazorWebApp.Client.Services;

public class CompanyService
{
    private readonly HttpClient _httpClient;

    public CompanyService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<CustomerCompanyDto>> GetAllAsync()
    {
        var companies = await _httpClient.GetFromJsonAsync<List<CustomerCompanyDto>>("api/Companies");
        return companies ?? new List<CustomerCompanyDto>();
    }

    public async Task<CustomerCompanyDto?> CreateAsync(CreateCompanyRequest request)
    {
        var response = await _httpClient.PostAsJsonAsync("api/Companies", request);
        if (!response.IsSuccessStatusCode) return null;
        return await response.Content.ReadFromJsonAsync<CustomerCompanyDto>();
    }
}
