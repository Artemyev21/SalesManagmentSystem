using System;

namespace SalesManagementSystemWebApi2.BLL.Exceptions
{
    public class SalesPointNotFoundException : Exception
    {
        public SalesPointNotFoundException(string message) : base(message) { }
    }
}
