using System.Collections.Generic;

namespace ReceiptGenerator.Models
{
    public class ShoppingBasket
    {
        public IEnumerable<ShoppingBasketItem> Items { get; set; }
    }
}
