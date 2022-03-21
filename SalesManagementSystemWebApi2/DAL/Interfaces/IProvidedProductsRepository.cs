using SalesManagementSystemWebApi2.DAL.Models;

namespace SalesManagementSystemWebApi2.DAL.Interfaces
{
    public interface IProvidedProductsRepository
    {
        public ProvidedProduct[] Query(int salesPointId, int productId);

        public void Update(int salesPointId, int productId, int productQuantity);

        public bool Create(int salesPointId, int productId, int productQuantity);

        public void DeleteAll();
    }
}
