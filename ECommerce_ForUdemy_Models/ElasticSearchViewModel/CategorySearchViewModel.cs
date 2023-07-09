using Nest;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce_ForUdemy_Models.ElasticSearchViewModel
{
    public class CategorySearchViewModel
    {
        [Display(Name = "ProductName")]
        public string? CategoryName { get; set; }
        [Display(Name = "Created  Date")]

        public DateTime? CreatedDate { get; set; }
    }
}
