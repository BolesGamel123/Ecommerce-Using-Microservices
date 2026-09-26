using MediatR;
using Ordering.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Queries
{
    public class GetAllOrderByUsernameQuery:IRequest<IList<OrderResponse>>
    {
        public string UserName { get; set; }
        public GetAllOrderByUsernameQuery(string userName)
        {
            UserName = userName;
        }
    }
}
