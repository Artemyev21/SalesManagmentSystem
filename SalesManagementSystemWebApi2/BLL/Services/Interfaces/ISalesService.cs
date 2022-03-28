using SalesManagementSystemWebApi2.BLL.Models;

namespace SalesManagementSystemWebApi2.BLL.Services.Interfaces
{
    public interface ISalesService
    {
        Sale BuyProducts(BuyProductsModel model);

        bool Test();

        bool PopulateDB();
    }
}
