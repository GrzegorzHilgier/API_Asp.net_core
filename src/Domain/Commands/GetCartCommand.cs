using System;
using System.Collections.Generic;
using System.Text;
using Domain.Responses.Cart;
using MediatR;

namespace Domain.Commands
{
    public class GetCartCommand : IRequest<CartSessionResponse>
    {
        public Guid Id;
    }
}
