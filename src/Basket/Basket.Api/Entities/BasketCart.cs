using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Basket.Api.Entities
{
    public class BasketCart
    {
        public BasketCart()
        {

        }
        public BasketCart(string userName)
        {
            UserName = userName;
        }
        public string UserName { get; set; }
        public List<BasketCartItem> Items { get; set; } = new List<BasketCartItem>();
        public decimal TotalPrice { 
            get
            {
                return Items.Sum(x => x.Price * x.Quantity);
            }
        }
    }
}
