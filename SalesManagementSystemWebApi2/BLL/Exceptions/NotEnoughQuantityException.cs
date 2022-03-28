using System;

namespace SalesManagementSystemWebApi2.BLL.Exceptions
{
    public class NotEnoughQuantityException : Exception
    {
        public NotEnoughQuantityException(string message) : base(message) { }
    }
}
