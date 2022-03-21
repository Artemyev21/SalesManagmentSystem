using SalesManagementSystemWebApi2.DAL.Interfaces;
using System.Data; 
using Dapper;
using Microsoft.Data.SqlClient;
using System.Linq;

namespace SalesManagementSystemWebApi2.DAL.Models
{
    public class SalesDataRepository : ISalesDataRepository
    {
        private readonly string _connection;

        public SalesDataRepository(string connection)
        {
            _connection = connection;
        }

        public void Create(int saleId, int productId, int productQuantity, int productAmount)
        {
            using (IDbConnection db = new SqlConnection(_connection))
            {
                var sqlQuery = $"Insert into SalesData(SalesId, ProductId, ProductQuantity, ProductAmount) " +
                               $"Values({saleId},{productId},{productQuantity},{productAmount})";
                db.Execute(sqlQuery);                
            }
        }
    }
}
