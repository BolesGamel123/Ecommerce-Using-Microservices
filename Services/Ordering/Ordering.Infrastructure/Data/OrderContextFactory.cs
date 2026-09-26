using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Infrastructure.Data
{
    public class OrderContextFactory : IDesignTimeDbContextFactory<OrderContext>
    {
        public OrderContext CreateDbContext(string[] args)
        {
            var option = new DbContextOptionsBuilder<OrderContext>();
            option.UseSqlServer("Server=localhost,1433;Database=OrderDb;User Id=sa;Password=P@ssw0rd123;TrustServerCertificate=True;");
            return new OrderContext(option.Options);
        }
    }
}
