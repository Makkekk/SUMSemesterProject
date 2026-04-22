using System.Collections.Generic;
using System.Net.Http.Json;
using DTO;

namespace LajmiBlazorWebApp.Client.Services;

public class ProductService
{
    private readonly HttpClient _http;

    public ProductService(HttpClient http)
    {
        _http = http;
    }

    public async Task<List<ProductDto>> GetProductsAsync()
    {
        try
        {
            var products = await _http.GetFromJsonAsync<List<ProductDto>>("api/products");
            return products ?? new List<ProductDto>();
        }
        catch (Exception ex)
        {
            // Log error here if needed
            Console.WriteLine($"Error fetching products: {ex.Message}");
            return new List<ProductDto>();
        }
    }

    public async Task<ProductDto?> GetProductByIdAsync(Guid id)
    {
        try
        {
            return await _http.GetFromJsonAsync<ProductDto>($"api/products/{id}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching product {id}: {ex.Message}");
            return null;
        }
    }

    public async Task<ProductDto?> CreateProductAsync(ProductDto productDto)
    {
        try
        {
            var response = await _http.PostAsJsonAsync("api/products", productDto);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<ProductDto>();
            }
            return null;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error creating product: {ex.Message}");
            return null;
        }
    }
    
}
