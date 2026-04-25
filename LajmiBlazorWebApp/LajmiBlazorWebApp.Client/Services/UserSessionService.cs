using DTO;

namespace LajmiBlazorWebApp.Client.Services;

public class UserSessionService
{
    public UserDto? CurrentUser { get; private set; }

    public bool IsLoggedIn => CurrentUser != null;

    public void Login(UserDto user)
    {
        CurrentUser = user;
    }

    public void Logout()
    {
        CurrentUser = null;
    }
}  
