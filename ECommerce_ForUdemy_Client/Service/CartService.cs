using Blazored.LocalStorage;
using E_CommerceForUdemy_Common;
using ECommerce_ForUdemy_Client.Service.IService;
using ECommerce_ForUdemy_Client.ViewModels;

namespace ECommerce_ForUdemy_Client.Service
{
    public class CartService : ICartService
    {
        private readonly ILocalStorageService _localStorageService;
        public event Action OnChange;
        
        public CartService(ILocalStorageService localStorageService)
        {
            _localStorageService = localStorageService;
        }

        public async Task Decrement(ShoppingCart cartToDecrement)
        {
            var cart = await _localStorageService.GetItemAsync<List<ShoppingCart>>(Keys.ShoppingCart);

            //if count is 0 or 1 then we remove the item
            for (int i = 0; i < cart.Count; i++)
            {
                if (cart[i].ProductId == cartToDecrement.ProductId && cart[i].ProductPriceId == cartToDecrement.ProductPriceId)
                {
                    if (cart[i].Count == 1 || cartToDecrement.Count == 0)
                    {
                        cart.Remove(cart[i]);
                    }
                    else
                    {
                        cart[i].Count -= cartToDecrement.Count;
                    }
                }
            }

            await _localStorageService.SetItemAsync(Keys.ShoppingCart, cart);
            OnChange.Invoke();
        }

        public async Task Increment(ShoppingCart cartToAdd)
        {
            var cart = await _localStorageService.GetItemAsync<List<ShoppingCart>>(Keys.ShoppingCart);
            bool itemInCart = false;

            if (cart == null)
            {
                cart = new List<ShoppingCart>();
            }
            foreach (var obj in cart)
            {
                if (obj.ProductId == cartToAdd.ProductId && obj.ProductPriceId == cartToAdd.ProductPriceId)
                {
                    itemInCart = true;
                    obj.Count += cartToAdd.Count;
                }
            }
            if (!itemInCart)
            {
                cart.Add(new ShoppingCart()
                {
                    ProductId = cartToAdd.ProductId,
                    ProductPriceId = cartToAdd.ProductPriceId,
                    Count = cartToAdd.Count
                });
            }
            await _localStorageService.SetItemAsync(Keys.ShoppingCart, cart);
            OnChange.Invoke();
        }
    }
}
