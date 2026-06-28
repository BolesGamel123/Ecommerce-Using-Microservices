using Basket.Core.Entities;

namespace Basket.Application.Responses
{
    public class ShoppingCartResponse
    {
        public string UserName { get; set; }

        public List<ShoppingCartItem> Items { get; set; } = new List<ShoppingCartItem>();
        public decimal TotalPrice
        {
            get
            {
                decimal TotalPrice = 0;
                foreach (ShoppingCartItem item in Items)
                {
                    TotalPrice += item.Price * item.Quantity;
                }
                return TotalPrice;
            }
        }

        public ShoppingCartResponse()
        {

        }
        public ShoppingCartResponse(string userName)
        {
            UserName = userName;
        }
    }
}
