using Microsoft.EntityFrameworkCore;
using Ordering.Core.Entities;
using Ordering.Core.Repositories;
using Ordering.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Infrastructure.Repositories
{
    public class OrderingRepository : AsyncRepository<Order>, IOrderingRepository
    {
        public OrderingRepository(OrderContext _context) : base(_context)
        {
        }

        public async Task<IEnumerable<Order>> GetAllByUsernameAsync(string username)
        {
            return await context.Orders.Where(o=>o.UserName==username).ToListAsync();
        }
    }
}
