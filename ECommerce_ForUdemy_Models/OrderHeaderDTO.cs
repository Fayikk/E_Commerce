using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceForUdemy_DataAccess
{
    public class OrderHeaderDTO : BaseMessage
    {
        public int Id { get; set; }
        [Required]
        public string UserId { get; set; }
        //add navigation
        [Required]
        [Display(Name ="Order Total")]
        public double OrderTotal { get; set; }
        [Required]
        public DateTime OrderDate { get; set; }
        [Required]
        [Display(Name ="Shipping Date")]
        public DateTime ShippingDate { get; set; }
        [Required]
        public string Status { get; set; }
        //Sripe
        public string? SessionId { get; set; }
        public string? PaymentIntentId { get; set; }
        //User
        [Required]
        public string FirstName { get; set; }
        [Required]

        public string LastName { get; set; }    
        [Required]
        public string Address { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public string PostalCode { get; set; }
        [Required]
        public string AdditionalInformation { get; set; }
        [Required]  
        public string Email { get; set; }

    }
}
