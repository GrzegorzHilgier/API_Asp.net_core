using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Commands;
using Domain.Entities;
using Domain.Repositories;
using Domain.Responses.Cart;
using Domain.Services;
using MediatR;

namespace Domain.Handlers
{
    public class GetCartsHandler: IRequestHandler<GetCartsCommand, PaginatedEntity<Guid>>
    {
        private readonly ICartRepository _repository;


        public GetCartsHandler(ICartRepository repository)
        {
            _repository = repository;

        }

        public async Task<PaginatedEntity<Guid>> Handle(GetCartsCommand request, CancellationToken cancellationToken)
        {
            var result = await _repository.GetCarts(request.PageSize, request.PageIndex);
            var count = _repository.Count;
            var response = new PaginatedEntity<Guid>(request.PageIndex, request.PageSize, count, result);
            return response;
        }
    }
}
