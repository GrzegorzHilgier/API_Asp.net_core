using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.Cart
{
    public class CartItem
    {
        public Guid Id { get; set; }
        public Guid ItemId { get; set; }
        public int Quantity { get; set; }
        public CartSession CartSession { get; set; }
        public Guid CartSessionId { get; set; }
        public void IncreaseQuantity()
        {
            Quantity++;
        }
        public void DecreaseQuantity()
        {
            Quantity--;
        }
    }
}
