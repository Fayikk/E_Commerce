using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceForUdemy_DataAccess
{
    public class Discount : BaseMessage
    {
        [Key]
        public int Id { get; set; } 
        public string CouponCode { get; set; }
        public int AmountDiscount { get; set; } 
    }
}
