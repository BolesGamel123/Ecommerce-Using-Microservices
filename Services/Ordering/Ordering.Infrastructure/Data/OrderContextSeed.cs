using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Ordering.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Infrastructure.Data
{
    public  class OrderContextSeed
    {
        public static async Task SeedAsync(OrderContext context,ILogger<OrderContextSeed> logger)
        {
            if (await context.Orders.AnyAsync())
            {
                logger.LogInformation("Data already seeded.");
                return;
            }

            var orders = new List<Order>
            {
                new Order
                {
                    UserName = "ahmed_mohamed",
                    TotalPrice = 250.50m,
                    FirstName = "Ahmed",
                    LastName = "Mohamed",
                    Email = "ahmed@example.com",
                    AddressLine = "123 Main St",
                    State = "Cairo",
                    ZipCode = "11511",
                    CardName = "Ahmed Mohamed",
                    CardNumber = "4111111111111111",
                    Expiration = "12/26",
                    Cvv = "123",
                    PaymentMethod = 1
                },
                new Order
                {
                    UserName = "sara_ali",
                    TotalPrice = 499.99m,
                    FirstName = "Sara",
                    LastName = "Ali",
                    Email = "sara@example.com",
                    AddressLine = "45 Nile Street",
                    State = "Giza",
                    ZipCode = "12111",
                    CardName = "Sara Ali",
                    CardNumber = "4222222222222222",
                    Expiration = "08/27",
                    Cvv = "456",
                    PaymentMethod = 2
                },
                new Order
                {
                    UserName = "mohamed_hassan",
                    TotalPrice = 799.00m,
                    FirstName = "Mohamed",
                    LastName = "Hassan",
                    Email = "mohamed.hassan@example.com",
                    AddressLine = "78 Tahrir Square",
                    State = "Cairo",
                    ZipCode = "11511",
                    CardName = "Mohamed Hassan",
                    CardNumber = "4333333333333333",
                    Expiration = "05/28",
                    Cvv = "789",
                    PaymentMethod = 1
                }
            };

            await context.Orders.AddRangeAsync(orders);
            await context.SaveChangesAsync();
        }
    }
}
