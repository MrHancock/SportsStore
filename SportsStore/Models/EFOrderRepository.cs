using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Models
{
    public class EFOrderRepository : IOrderRepository
    {
        private ApplicationDbContext context;
        public EFOrderRepository(ApplicationDbContext cnt) => context = cnt;
        IQueryable<Order> IOrderRepository.Orders => context.Orders
                .Include(o => o.Lines)
                .ThenInclude(l => l.Product);

        public void SaveOrder(Order order)
        {
            context.AttachRange(order.Lines.Select(l => l.Product));
            if (order.ID == 0)
            {
                context.Orders.Add(order);
            }
            context.SaveChanges();
        }
    }
}
