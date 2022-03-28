using System;

namespace SalesManagementSystemWebApi2.BLL.Exceptions
{
    public class ProductNotFoundException : Exception
    {
        public ProductNotFoundException(string message) : base(message) { }        
    }
}
