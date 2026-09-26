using AutoMapper;
using MediatR;
using Ordering.Application.Queries;
using Ordering.Application.Responses;
using Ordering.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Handlers.Queries
{
    public class GetAllOrderByUsernameQueryHandler : IRequestHandler<GetAllOrderByUsernameQuery, IList<OrderResponse>>
    {
        private readonly IOrderingRepository _orderingRepository;
        private readonly IMapper _mapper;
        public GetAllOrderByUsernameQueryHandler(IOrderingRepository orderingRepository, IMapper mapper)
        {
            _orderingRepository= orderingRepository;
            _mapper = mapper;
        }
        public async Task<IList<OrderResponse>> Handle(GetAllOrderByUsernameQuery request, CancellationToken cancellationToken)
        {
            var orders = await _orderingRepository.GetAllByUsernameAsync(request.UserName);
            return  _mapper.Map<IList<OrderResponse>>(orders);
        }
    }
}
