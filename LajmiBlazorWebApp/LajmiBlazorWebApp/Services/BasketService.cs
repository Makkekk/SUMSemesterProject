namespace LajmiBlazorWebApp.Services;

public class BasketService
{
    
    // Her sikre vi at kun metoderne i denne klasse kan ændre NumOfProducts. Indkapsling
    private int _NumOfProducts = 0;
    
    public int NumOfProducts => _NumOfProducts;

    public event Action? OnChange;


    public void AddProductcounter()
    {
        _NumOfProducts++;
        NotifyStateChanged();
    }
    
    
    public void NotifyStateChanged() => OnChange?.Invoke();

}