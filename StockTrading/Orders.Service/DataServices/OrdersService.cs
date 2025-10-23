using Microsoft.EntityFrameworkCore;
using Orders.Service.Data;
using Orders.Service.DataServices.Contracts;
using Orders.Service.Entities;
using Orders.Service.Entities.Enums;
using Orders.Service.Infrastructure.Exceptions;
using Orders.Service.ViewModels;

namespace Orders.Service.DataServices;

public class OrdersService : IOrdersService
{
    private readonly OrdersDbContext dbContext;

    public OrdersService(OrdersDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task Create(string userId, OrderCreateViewModel order)
    {
        OrderEntity orderEntity = new()
        {
            UserId = userId,
            Quantity = order.Quantity,
            TickerId = await this.GetTickerId(order.Ticker!),
            Side = (SideEnum)Enum.Parse(typeof(SideEnum), order.Side!)
        };

        await this.dbContext.AddAsync(orderEntity);

        await this.dbContext.SaveChangesAsync();
    }

    private async Task<int> GetTickerId(string ticker)
    {
        int? tickerId = await this.dbContext
            .Stocks
            .Where(s => s.Ticker == ticker)
            .Select(s => (int?)s.Id)
            .SingleOrDefaultAsync();

        if (!tickerId.HasValue)
        {
            throw new ActionableException("Invalid ticker!");
        }

        return tickerId.Value;
    }
}
