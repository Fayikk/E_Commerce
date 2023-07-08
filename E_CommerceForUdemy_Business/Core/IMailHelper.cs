using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceForUdemy_Business.Core
{
    public interface IMailHelper
    {
        public void SendEmailForOrder(string subject, string body, string mail);
    }
}
