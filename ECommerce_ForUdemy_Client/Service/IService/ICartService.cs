﻿using ECommerce_ForUdemy_Client.ViewModels;

namespace ECommerce_ForUdemy_Client.Service.IService
{
    public interface ICartService
    {
        public event Action OnChange;
        Task Decrement(ShoppingCart shoppingCart);
        Task Increment(ShoppingCart shoppingCart);  
    }
}
