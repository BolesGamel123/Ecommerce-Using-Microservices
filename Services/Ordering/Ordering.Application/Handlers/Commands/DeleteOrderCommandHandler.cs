using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Commands;
using Ordering.Application.Exceptions;
using Ordering.Core.Entities;
using Ordering.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Handlers.Commands
{
    public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand, Unit>
    {
        private readonly IOrderingRepository _orderingRepository;
        private readonly ILogger<DeleteOrderCommandHandler> _logger;
        public DeleteOrderCommandHandler(IOrderingRepository orderingRepository, ILogger<DeleteOrderCommandHandler> logger)
        {
            _orderingRepository = orderingRepository;
            _logger = logger;
        }
        public async Task<Unit> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _orderingRepository.GetByIdAsync(request.Id);
            if (order == null)
            {
                throw new OrderNotFoundException(nameof(Order), request.Id);
            }

            await _orderingRepository.DeleteEntityAsync(order);
            _logger.LogInformation($"Order With Id {order.Id} Successfully Remove");
            return Unit.Value;
        }
    }
}
