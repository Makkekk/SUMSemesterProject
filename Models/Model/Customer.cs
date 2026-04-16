namespace Models;

public class Customer
{
    public Guid CustomerId { get; set; }
    public string CustomerName { get; set; }
    public List<Order> Orders { get; set; }
    public Company Company { get; set; }
    public string CustomerEmail { get; set; }
    public string CustomerPhoneNumber { get; set; }
    public DiscountAgreement CustomerAgreementDescription { get; set; }
    public List<FavoriteProduct> FavoriteProducts { get; set; }
}