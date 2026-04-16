namespace Models;

public class FavoriteProduct
{
    public Guid FavoriteProductId { get; set; }
    public Product Product { get; set; }
    public User User { get; set; }
}