using AutoMapper;
using Basket.Application.Queries;
using Basket.Application.Responses;
using Basket.Core.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basket.Application.Handlers.Queries
{
    public class GetBasketQueryHandler : IRequestHandler<GetBasketQuery, ShoppingCartResponse>
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IMapper _mapper;
        public GetBasketQueryHandler(IBasketRepository basketRepository, IMapper mapper)
        {
            _basketRepository = basketRepository;
            _mapper = mapper;
        }
        public async Task<ShoppingCartResponse> Handle(GetBasketQuery request, CancellationToken cancellationToken)
        {
            var shopCart= await _basketRepository.GetBasket(request.UserName);
            return _mapper.Map<ShoppingCartResponse>(shopCart);
        }
    }
}
