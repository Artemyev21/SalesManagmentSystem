namespace SalesManagementSystemWebApi2.DAL.Models
{
    public class SalesData
    {
        public int ProductId { get; set; }

        public int ProductQuantity { get; set; } //– количество штук купленных продуктов данного ProductId

        public int ProductAmount { get; set; } //– обща стоимость купленного количества товаров данного ProductId
    }
}
