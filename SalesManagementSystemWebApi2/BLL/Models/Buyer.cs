using System.Collections.Generic;

namespace SalesManagementSystemWebApi2.BLL.Models
{
    public class Buyer 
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<int> SalesIds { get; set; }//коллекция всех идентификаторов покупок,
                                                //когда-либо осуществляемых данным
                                                //покупателем
    }
}
