using Nest;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce_ForUdemy_Models.ElasticSearchViewModel
{
    public class ProductSearchViewModel
    {

        [Display(Name = "ProductName")]
        public string? ProductName { get; set; }
        [Display(Name = "Product Description")]

        public string? ProductDescription { get; set; }
        [Display(Name = "Color")]
        public string? Color { get; set; }
    }
}
