using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceForUdemy_Common
{
    public static class Keys
    {
        public const string ShoppingCart = "ShoppingCart";

        public const string Status_Pending = "Pending";
        public const string Status_Confirmed = "Confirmed";
        public const string Status_Shipped = "Shipped";
        public const string Status_Refunded = "Refunded";
        public const string Status_Cancelled = "Cancelled";

        public const string Role_Admin = "Admin";
        public const string Role_Customer = "Customer";

        public const string Local_Token = "Jwt_Token";
        public const string Local_UserDetails = "UserDetails";
        public const string Local_OrderDetails = "OrderDetails";
    }
}
