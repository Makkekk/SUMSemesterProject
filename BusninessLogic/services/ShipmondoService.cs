using Models;

namespace BusinessLogic.Services;

public class ShipmondoService
{
    public async Task<OrderLabel> CreateShipment(Order order)
    {
        // Fake version (før API)
        return new OrderLabel
        {
            OrderLabelId = Guid.NewGuid(),
            TrackingNumber = "TEST-" + new Random().Next(1000, 9999),
            Carrier = "GLS",
            OrderId = order.OrderId
        };
    }
}