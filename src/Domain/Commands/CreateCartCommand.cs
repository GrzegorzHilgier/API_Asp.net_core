using System;
using System.Collections.Generic;
using System.Text;
using Domain.Requests.Item;
using Domain.Responses.Cart;
using MediatR;

namespace Domain.Commands
{
    public class CreateCartCommand : IRequest<CartSessionResponse>
    {
        public IEnumerable<Guid> ItemsIds { get; set; }

        public string UserEmail { get; set; }
    }
}
