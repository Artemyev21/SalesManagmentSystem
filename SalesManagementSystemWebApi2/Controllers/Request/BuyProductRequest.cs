namespace SalesManagementSystemWebApi2.Controllers.Request
{
    public class BuyProductRequest 
    {
        public int BuyerId { get; set; }

        public int ProductId { get; set; }

        public int SalesPointId { get; set; }

        public int Quantity { get; set; }
        
    }
}
