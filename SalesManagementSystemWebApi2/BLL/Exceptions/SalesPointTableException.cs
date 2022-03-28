using System;

namespace SalesManagementSystemWebApi2.BLL.Exceptions
{
    public class SalesPointTableException : Exception
    {
        public SalesPointTableException(string message) : base(message) { }
    }
}
