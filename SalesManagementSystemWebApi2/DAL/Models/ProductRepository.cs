using SalesManagementSystemWebApi2.DAL.Interfaces;
using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Linq;

namespace SalesManagementSystemWebApi2.DAL.Models
{
    public class ProductRepository : IProductRepository
    {
        private readonly string _connection;
        public ProductRepository(string connection)
        {
            _connection = connection;
        }

        public bool CheckExists(long id)           
        {
            ///тут должен быть сформирован запрос в БД
            ///те у нас есть id продукта, мы хотим проверить в БД в таблице Product
            ///есть ли в таблице продукт с таким id

            using (IDbConnection db = new SqlConnection(_connection))
            {
                var sqlQuery = $"Select Name From Product Where Product.id = {id}";
                var result = db.Query<string>(sqlQuery);
                return result.Any();
            }            
        }

        public bool Create(string name, int price)
        {
            using (IDbConnection db = new SqlConnection(_connection))
            {
                var sqlQuery = $"Insert Into Product(Name,Price) " +
                               $"OUTPUT Inserted.ID " +
                               $"Values('{name}',{price})";
                var resQuery = db.Query<int>(sqlQuery);
                return resQuery.Any();
            }                
        }

        public void DeleteAll()
        {
            using (IDbConnection db = new SqlConnection(_connection))
            {
                var sqlQuery = $"truncate table Product";
                db.Execute(sqlQuery);
            }
        }

        public Product Query(int id)
        {
            using (IDbConnection db = new SqlConnection(_connection))
            {
                var sqlQuery = $"Select * " +
                               $"From Product " +
                               $"Where Product.Id = {id}";
                return db.Query<Product>(sqlQuery).FirstOrDefault();
            }
        }        
    }
}
