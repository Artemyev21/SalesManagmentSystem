using System;

namespace SalesManagementSystemWebApi2.BLL.Exceptions
{
    public class NotUpdateTableException : Exception
    {
        public NotUpdateTableException(string message) : base(message)
        {
            //
        }
    }
}
