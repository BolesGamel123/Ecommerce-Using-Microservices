using AutoMapper;
using Discount.Application.Commands;
using Discount.Application.Handlers.Queries;
using Discount.Application.Queries;
using Discount.Core.Entities;
using Discount.Core.Repositories;
using Discount.Grpc.proto3;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discount.Application.Handlers.Commands
{
    public class DeleteDiscountCommandHandler : IRequestHandler<DeleteDiscountCommand, bool>
    {
        private readonly IDiscountRepository _discountRepository;
        private readonly IMapper _mapper;
        public DeleteDiscountCommandHandler(IDiscountRepository discountRepository,  IMapper mapper)
        {
            _discountRepository = discountRepository;
            _mapper = mapper;
        }

        public async Task<bool> Handle(DeleteDiscountCommand request, CancellationToken cancellationToken)
        {     
           var deleted= await _discountRepository.DeleteDiscount(request.ProductName);
            return deleted;
        }
    }
}
