using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesManagementSystemWebApi2.BLL.Exceptions
{
    public class DataNonAddedException : Exception
    {
        public DataNonAddedException(string message) : base(message)
        {       
            //            
        }
    }
}
