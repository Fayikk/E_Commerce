using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce_ForUdemy_Models
{
    public class ErrorModelDTO
    {
        public string Title { get; set; }   
        public int StatusCode { get; set; }
        public string ErrorMessage { get; set; } 
    }
}
