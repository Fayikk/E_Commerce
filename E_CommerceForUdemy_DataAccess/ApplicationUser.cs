using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceForUdemy_DataAccess
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
        public Guid ForgotPasswordNumber { get; set; }    
    }
}
