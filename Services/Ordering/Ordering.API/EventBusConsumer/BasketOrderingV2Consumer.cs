using AutoMapper;
using EventBus.Messages.Events;
using MassTransit;
using MediatR;
using Ordering.Application.Commands;

namespace Ordering.API.EventBusConsumer
{
    public class BasketOrderingV2Consumer : IConsumer<BasketCheckoutEventV2>
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly ILogger<BasketOrderingV2Consumer> _logger;
        public BasketOrderingV2Consumer(
            IMediator mediator,
            IMapper mapper,
            ILogger<BasketOrderingV2Consumer> logger
            )
        {
            _mediator = mediator;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task Consume(ConsumeContext<BasketCheckoutEventV2> context)
        {
            using var scope = _logger.BeginScope("consuming basket checkout event for {correlationid}", context.Message.CorrelationId);
            var cmd = _mapper.Map<CheckoutCommandV2>(context.Message);
            var result = await _mediator.Send(cmd);
            _logger.LogInformation("Basket checkout event completed !!");
        }
    }

}
