using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.Cart
{
    public class CartSession
    {
        public Guid Id { get; set; }
        public ICollection<CartItem> Items { get; set; }
        public CartUser User { get; set; }
        public string UserId { get; set; }
        public DateTimeOffset ValidityDate { get; set; }
    }
}
