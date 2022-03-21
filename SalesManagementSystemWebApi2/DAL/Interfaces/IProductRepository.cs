using SalesManagementSystemWebApi2.DAL.Models;

namespace SalesManagementSystemWebApi2.DAL.Interfaces
{
    public interface IProductRepository
    {
        public bool CheckExists(long id);

        public bool Create(string name, int price);

        public void DeleteAll();

        public Product Query(int id);
    }   
}
