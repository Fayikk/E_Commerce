using E_CommerceForUdemy_DataAccess;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce_ForUdemy_Models
{
    public class ProductDTO
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]

        public string Description { get; set; }
        public bool ShopFavourites { get; set; }
        public bool CustomerFavourites { get; set; }
        public string Color { get; set; }
        public string ImageUrl { get; set; }
        [Range(1,int.MaxValue,ErrorMessage ="Please Select a category")]
        public int CategoryId { get; set; }
        public CategoryDTO Category { get; set; }
    }
}

