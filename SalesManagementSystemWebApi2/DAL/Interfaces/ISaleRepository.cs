using System;

namespace SalesManagementSystemWebApi2.DAL.Interfaces
{
    public interface ISaleRepository
    {
        public int Create(DateTime dateTime, int salesPointId, int buyerId, int totalAmount);        
    }
}
