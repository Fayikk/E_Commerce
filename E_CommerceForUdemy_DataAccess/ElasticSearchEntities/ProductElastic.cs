using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceForUdemy_DataAccess.ElasticSearchEntities
{
    public class ProductElastic
    {
        [PropertyName("_id")]
        public int Id { get; set; }
        [PropertyName("productName")]

        public string ProductName { get; set; }
        [PropertyName("productDescription")]

        public string ProductDescription { get; set; }
        [PropertyName("color")]

        public string Color { get; set; }   
    }
}
