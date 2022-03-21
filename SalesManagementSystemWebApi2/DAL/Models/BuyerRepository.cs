using SalesManagementSystemWebApi2.DAL.Interfaces;
using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Linq;

namespace SalesManagementSystemWebApi2.DAL.Models
{
    public class BuyerRepository : IBuyerRepository
    {
        private readonly string _connection;

        public BuyerRepository(string connection)
        {
            _connection = connection;
        }

        public bool CheckExists(long id)
        {
            using (IDbConnection db = new SqlConnection(_connection))
            {
                var sqlQuery = $"Select Name" +
                               $"From Buyer" +
                               $"Where Buyer.ID = {id}";
                var result = db.Query<string>(sqlQuery);
                return result.Any();
            }
        }

        public int Create(string name)
        {
            using (IDbConnection db = new SqlConnection(_connection))
            {
                var sqlQuery = $"Insert into Buyer(Name) " +
                               $"OUTPUT Inserted.ID" +
                               $"Values('{name}')";
                return db.Query<int>(sqlQuery).FirstOrDefault();
            }
        }

        public void DeleteAll()
        {
            using (IDbConnection db = new SqlConnection(_connection))
            {
                
            }
        }
    }
}
