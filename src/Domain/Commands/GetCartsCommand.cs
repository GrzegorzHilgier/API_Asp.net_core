using System;
using System.Collections.Generic;
using System.Text;
using Domain.Entities;
using Domain.Entities.Cart;
using Domain.Responses.Cart;
using MediatR;

namespace Domain.Commands
{
    public class GetCartsCommand : IRequest<PaginatedEntity<Guid>>
    {
        public int PageSize;
        public int PageIndex;
    }
}
