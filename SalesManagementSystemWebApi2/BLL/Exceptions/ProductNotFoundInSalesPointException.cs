using System;

namespace SalesManagementSystemWebApi2.BLL.Exceptions
{
    public class ProductNotFoundInSalesPointException : Exception
    {
        public ProductNotFoundInSalesPointException(string message) : base(message) { }
    }
}
