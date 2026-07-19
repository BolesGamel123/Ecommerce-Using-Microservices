using Discount.Grpc.proto3;
using Grpc.Core;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basket.Application.GrpcServices
{
    public class DiscountGrpcService
    {
        private readonly DiscountProtoService.DiscountProtoServiceClient _discountGrpcClient;
        public DiscountGrpcService(DiscountProtoService.DiscountProtoServiceClient discountGrpcClient)
        {
            _discountGrpcClient= discountGrpcClient;
        }

        public async Task<CouponModel> GetDiscount(string ProductName)
        {
            var discountRequest = new GetDiscountRequest{ProductName=ProductName};
            return await _discountGrpcClient.GetDiscountAsync(discountRequest);
        }
    }
}
