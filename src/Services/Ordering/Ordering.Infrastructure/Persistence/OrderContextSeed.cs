using Microsoft.Extensions.Logging;
using Ordering.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Infrastructure.Persistence
{
    public class OrderContextSeed
    {
        public static async Task SeedAsync(OrderContext orderContext, ILogger<OrderContextSeed> logger)
        {
            if (!orderContext.Orders.Any())
            {
                orderContext.Orders.AddRange(GetPreConfiguredOrders());
                await orderContext.SaveChangesAsync();
                logger.LogInformation("Successfully Seeded database associated with context {DbContextName}", 
                    typeof(OrderContext).Name);
            }
        }

        private static IEnumerable<Order> GetPreConfiguredOrders()
        {
            return new List<Order> {
                new Order()
                {
                    UserName = "chandan",
                    FirstName = "Chandan",
                    LastName = "Sah",
                    EmailAddress = "chandansah625@gmail.com", 
                    AddressLine = "XYZ", 
                    Country = "India", 
                    TotalPrice = 350
                }
            };
        }
    }
}
