using System.Collections.Generic;

namespace SalesManagementSystemWebApi2.BLL.Models
{
    public class SalesPoint
    {        
        public int Id { get; set; }
     
        public string Name { get; set; }
        
        public List<ProvidedProduct> ProvidedProducts { get; set; } 
    }
}
