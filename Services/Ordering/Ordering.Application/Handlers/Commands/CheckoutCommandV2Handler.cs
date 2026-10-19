using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Commands;
using Ordering.Core.Entities;
using Ordering.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Handlers.Commands
{
    public class CheckoutCommandV2Handler : IRequestHandler<CheckoutCommandV2, int>
    {
        private readonly IOrderingRepository _orderingRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CheckoutCommandV2Handler> _logger;
        public CheckoutCommandV2Handler(IOrderingRepository orderingRepository, IMapper mapper, ILogger<CheckoutCommandV2Handler> logger)
        {
            _orderingRepository = orderingRepository;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<int> Handle(CheckoutCommandV2 request, CancellationToken cancellationToken)
        {
            var order = _mapper.Map<Order>(request);
            var newOrder = await _orderingRepository.AddEntityAsync(order);
            _logger.LogInformation($"Order With Id {order.Id} Successfully Added");
            return order.Id;

        }
    }
}
