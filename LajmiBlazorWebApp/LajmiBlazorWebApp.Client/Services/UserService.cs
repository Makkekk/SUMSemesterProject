using System.ComponentModel.DataAnnotations;
using System.Net.Http.Json;
using DTO;

namespace LajmiBlazorWebApp.Client.Services;

public class UserService
{
  private readonly HttpClient _httpClient;
  
  public UserService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<UserDto> LoginAsync(string email, string password)
    {
        //fanger post request
        var response = await _httpClient.PostAsJsonAsync("api/user/login", new
        {
            Email = email, Password = password
        });
        // hvis bruger findes med email og kdoe 
        if (!response.IsSuccessStatusCode)
        {
            return null;
        }
    return await response.Content.ReadFromJsonAsync<UserDto?>();
    
    }
}