using System.ComponentModel.DataAnnotations;

namespace Models;

public class FavoriteProduct
{
    [Key]
    public Guid FavoriteProductId { get; set; }
    public Product Product { get; set; }
    public User User { get; set; }
}