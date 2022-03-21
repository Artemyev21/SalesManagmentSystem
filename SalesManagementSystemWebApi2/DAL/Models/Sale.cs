using System;

namespace SalesManagementSystemWebApi2.DAL.Models
{
    public class Sale
    {
        public int Id { get; set; }//– Идентификатор

        public DateTime Date { get; set; }//– дата и время осуществления продажи        

        public int SalesPointId { get; set; }//– идентификатор точки продажи

        public int BuyerId { get; set; }//идентификатор покупателя (Can by null)

        public SalesData SalesData { get; set; }//список сущностей SaleData

        public int TotalAmount { get; set; }//– общая сумма всей покупки
    }
}
