using System;
using System.Collections.Generic;
using System.Text;
using Domain.Entities.Cart;

namespace Domain.Responses.Cart
{
    public class CartSessionResponse
    {
        public string Id { get; set; }

        public IList<CartItemResponse> Items { get; set; }

        public CartUser User { get; set; }

        public DateTimeOffset ValidityDate { get; set; }
    }
}
