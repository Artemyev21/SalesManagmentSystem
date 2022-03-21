using System;

namespace SalesManagementSystemWebApi2.Controllers.Response
{
    public class BuyProductResponse
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public int SalesPointId { get; set; }

        public int BuyerId { get; set; }

        public SalesData SalesInfo { get; set; }

        public int TotalAmount { get; set; }//– общая сумма всей покупки

        public class SalesData
        {
            public int ProductId { get; set; }

            public int ProductQuantity { get; set; } 

            public int ProductAmount { get; set; } //– обща стоимость купленного количества товаров данного ProductId
        }
    }
    
}
