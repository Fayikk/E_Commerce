using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce_ForUdemy_Models
{
    public class SignUpResponseDTO
    {
        public bool IsRegisterationSuccessful { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }
}
