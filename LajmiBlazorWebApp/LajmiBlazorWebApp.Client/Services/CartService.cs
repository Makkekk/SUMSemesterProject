using System.Collections.Generic;
using DTO;
using Models;

namespace LajmiBlazorWebApp.Client.Services;

public class CartService
{
    public List<ProductDto> Items { get; private set; } = new();

    // Her sikre vi at kun metoderne i denne klasse kan ændre NumOfProducts. Indkapsling
    private int _NumOfProducts = 0;

    public int NumOfProducts => _NumOfProducts; // --> Vi sender altid en reference ud og ikke det "rigtige" num

    public event Action? OnChange;


    public void AddProductcounter()
    {
        _NumOfProducts++;
        NotifyStateChanged();
    }

    public void AddProduct(ProductDto productDto)
    {
        Items.Add(productDto);
        NotifyStateChanged();
    }


    public void NotifyStateChanged() => OnChange?.Invoke();

    //burde den ændres til serverside
    public decimal GetDiscountedPrice(ProductDto productDto, decimal discount)
    {
        return productDto.ProductPrice * (1 - discount);
    }
    
    public decimal GetTotalPrice(decimal discount)
    {
        decimal totalPrice = 0;

        foreach (var product in Items)
        {
            totalPrice += GetDiscountedPrice(product, discount);
        }

        return totalPrice;
    }
}