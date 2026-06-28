using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basket.Application.Commands
{
    public class DeleteBasketCommand:IRequest<Unit>
    {
        public string UserName { get; set; }

        public DeleteBasketCommand(string userName)
        {
            UserName = userName;
        }
    }
}
