using SalesManagementSystemWebApi2.BLL.Models;

namespace SalesManagementSystemWebApi2.BLL.Services.Interfaces
{
    public interface ISalesService
    {
        Sale BuyProducts(BuyProductsModel model);

        bool Test();

        Product[] PopulateProduct();

        SalesPoint[] PopulateSalesPoint(Product[] products);

        void PopulateProvidedProducts(Product[] product, SalesPoint[] salesPoints);        
    }
}
