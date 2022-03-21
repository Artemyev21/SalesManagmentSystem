using SalesManagementSystemWebApi2.DAL.Interfaces;
using Microsoft.Data.SqlClient;
using Dapper;
using System.Data;
using System.Linq;


namespace SalesManagementSystemWebApi2.DAL.Models
{
    public class SalesPointRepository : ISalesPointRepository
    {
        private readonly string _connection;

        public SalesPointRepository(string connection)
        {
            _connection = connection;
        }

        public bool CheckExists(int id)
        {                     
            ///Те у нас есть id Точки продажи, мы хотим проверить в БД в таблице SalesPoint
            ///есть ли у нас в таблице точка продажи с таким id
            using (IDbConnection db = new SqlConnection(_connection))
            {
                var sqlQuery = $"Select Name From SalesPoint Where SalesPoint.Id = {id}";
                var result = db.Query<string>(sqlQuery);
                return result.Any();
            }            
        }

        public void DeleteAll()
        {
            using (IDbConnection db = new SqlConnection(_connection))
            {
                var sqlQuery = $"truncate table SalesPoint";
                db.Execute(sqlQuery);
            }
        }

        public bool Create(string name)
        {
            using (IDbConnection db = new SqlConnection(_connection))
            {
                var sqlQuery = $"Insert Into SalesPoint(Name) " +
                               $"OUTPUT Inserted.ID " +
                               $"Values('{name}')";

                var resQuery = db.Query<int>(sqlQuery);
                return resQuery.Any();
            }
        }
    }
}
