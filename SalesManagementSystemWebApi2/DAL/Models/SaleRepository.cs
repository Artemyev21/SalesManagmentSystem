using SalesManagementSystemWebApi2.DAL.Interfaces;
using System;
using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Linq;

namespace SalesManagementSystemWebApi2.DAL.Models
{
    public class SaleRepository : ISaleRepository
    {
        private readonly string _connection;

        public SaleRepository(string connection)
        {
            _connection = connection;
        }

        public int Create(DateTime dateTime, int salesPointId, int buyerId, int totalAmount)
        {            
            using (IDbConnection db = new SqlConnection(_connection))
            {
                var sqlQuery = $"Insert into Sale(Date,Time, SalesPointId, BuyerId, TotalAmount) " +
                               $"OUTPUT Inserted.ID " +
                               $"Values('{dateTime.Date.ToString()}', '{dateTime.TimeOfDay.ToString()}', {salesPointId}, {buyerId}, {totalAmount})";
                var result = db.Query<int>(sqlQuery).FirstOrDefault();
                return result;
            }
        }       
    }
}
