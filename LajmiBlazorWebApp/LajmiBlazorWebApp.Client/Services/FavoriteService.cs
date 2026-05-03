using DTO;

namespace LajmiBlazorWebApp.Client.Services;

public class FavoriteService
{
    private readonly UserService _userService;
    private readonly UserSessionService _userSessionService;
    private List<ProductDto> _favoriteProducts = new List<ProductDto>();
    
    public bool IsVisible { get; private set; }
    public IReadOnlyList<ProductDto> FavoriteProducts => _favoriteProducts;
    public event Action? OnFavoriteChanged;
    
    public FavoriteService(UserService userService, UserSessionService userSessionService) 
    {
        _userService = userService;
        _userSessionService = userSessionService;
        _userSessionService.OnChange += HandleSessionChange;
        
        // Load favorites if user is already logged in (e.g. during page refresh)
        if (_userSessionService.IsLoggedIn)
        {
            _ = LoadFavoriteProducts();
        }
    }

    private void HandleSessionChange()
    {
        if (_userSessionService.IsLoggedIn)
        {
            _ = LoadFavoriteProducts();
        }
        else
        {
            _favoriteProducts.Clear();
            OnFavoriteChanged?.Invoke();
        }
    }

    public async Task LoadFavoriteProducts()
    {
        if (_userSessionService?.CurrentUser != null)
        {
            _favoriteProducts = await _userService.GetFavoriteProductsAsync(_userSessionService.CurrentUser.UserId);
            OnFavoriteChanged?.Invoke();
        }
    }

    public async Task ToggleFavorite(ProductDto product)
    {
        if (_userSessionService?.CurrentUser == null)
            return;
        
        var existing = _favoriteProducts.FirstOrDefault(p => p.ProductId == product.ProductId);

        if (existing != null)
        {
            _favoriteProducts.Remove(existing);
        }
        else
        {
            _favoriteProducts.Add(product);
        }
        OnFavoriteChanged?.Invoke();
        await _userService.ToggleFavoriteAsync(_userSessionService.CurrentUser.UserId, product.ProductId);
    }
    
    public void ToggleSidebar()
    {
        IsVisible = !IsVisible;
        OnFavoriteChanged?.Invoke();
    }
    

}