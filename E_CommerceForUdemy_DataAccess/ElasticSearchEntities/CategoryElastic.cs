using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceForUdemy_DataAccess.ElasticSearchEntities
{
    public class CategoryElastic
    {
        [PropertyName("_id")]
        public int Id { get; set; }
        [PropertyName("name")]
        public string Name { get; set; }
        [PropertyName("createdDate")]
        public DateTime CreatedDate { get; set; }
    }
}
