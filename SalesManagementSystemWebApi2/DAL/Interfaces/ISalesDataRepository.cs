namespace SalesManagementSystemWebApi2.DAL.Interfaces
{
    public interface ISalesDataRepository
    {
        public void Create(int saleId, int productId, int productQuantity, int productAmount);
    }
}
