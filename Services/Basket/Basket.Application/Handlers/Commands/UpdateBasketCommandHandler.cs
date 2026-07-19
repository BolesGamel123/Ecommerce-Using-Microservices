using AutoMapper;
using Basket.Application.Commands;
using Basket.Application.GrpcServices;
using Basket.Application.Responses;
using Basket.Core.Entities;
using Basket.Core.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basket.Application.Handlers.Commands
{
    public class UpdateBasketCommandHandler : IRequestHandler<UpdateBasketCommand, ShoppingCartResponse>
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IMapper _mapper;
        private readonly DiscountGrpcService _discountGrpc;
        public UpdateBasketCommandHandler(IBasketRepository basketRepository, IMapper mapper, DiscountGrpcService discountGrpc)
        {
            _basketRepository = basketRepository;
            _mapper = mapper;
            _discountGrpc = discountGrpc;
        }     

        public async Task<ShoppingCartResponse> Handle(UpdateBasketCommand request, CancellationToken cancellationToken)
        {
            foreach (var item in request.Items)
            {
                Console.WriteLine($"Product: {item.ProductName}");
                var coupon = await _discountGrpc.GetDiscount(item.ProductName);
                if (coupon is not null)
                {
                    item.Price -= coupon.Amount;
                }
                Console.WriteLine($"Price After Discount: {item.Price}");

            }

            var shoppingCart = await _basketRepository.UpdateBasket(new ShoppingCart
            {
                UserName=request.UserName,
                Items=request.Items,
            });
            return _mapper.Map<ShoppingCartResponse>(shoppingCart);
        }
    }
}
