using System;
using System.Collections.Generic;
using System.Text;
using Domain.Responses.Cart;
using MediatR;

namespace Domain.Commands
{
    public class UpdateCartItemQuantityCommand : IRequest<CartSessionResponse>
    {
        public Guid CartId { get; set; }
        public Guid CartItemId { get; set; }
        public bool IsAddOperation { get; set; }
    }
}
