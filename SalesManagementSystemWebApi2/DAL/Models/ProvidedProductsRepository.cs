using SalesManagementSystemWebApi2.DAL.Interfaces;
using Microsoft.Data.SqlClient;
using Dapper;
using System.Data;
using System.Linq;

namespace SalesManagementSystemWebApi2.DAL.Models
{
    public class ProvidedProductsRepository : IProvidedProductsRepository
    {
        private readonly string _connection;

        public ProvidedProductsRepository(string connrction)
        {
            _connection = connrction;
        }

        public bool Create(int salesPointId, int productId, int productQuantity)
        {
            using (IDbConnection db = new SqlConnection(_connection))
            {
                var sqlQuery = $"Insert Into ProvidedProducts(SalesPointID,ProductID,ProductQuantity) " +
                               $"OUTPUT Inserted.SalesPointID " +
                               $"Values({salesPointId},{productId},{productQuantity})";
                var resQuery = db.Query<int>(sqlQuery);
                return resQuery.Any();
            }
        }

        public void DeleteAll()
        {            
            using (IDbConnection db = new SqlConnection(_connection))
            {
                var sqlQuery = $"truncate table ProvidedProducts";
                db.Execute(sqlQuery);
            }            
        }

        public void Update(int salesPointId, int productId, int productQuantity)
        {
            using (IDbConnection db = new SqlConnection(_connection))
            {
                var sqlQuery = $"Update ProvidedProducts " +                               
                               $"Set ProductQuantity = {productQuantity} " +                               
                               $"Where SalesPointID = {salesPointId} " +
                               $"And ProductID = {productId}";
               db.Execute(sqlQuery);                
            }
        }

        public ProvidedProduct[] Query(int salesPointId, int productId)
        {
            using (IDbConnection db = new SqlConnection(_connection))
            {
                var sqlQuery = $"Select ProductID, ProductQuantity " +
                               $"From ProvidedProducts " +
                               $"Where ProvidedProducts.SalesPointID = {salesPointId} and " +
                               $"ProvidedProducts.ProductID = {productId}";

                return db.Query<ProvidedProduct>(sqlQuery).ToArray();
            }
        }        
    }
}
