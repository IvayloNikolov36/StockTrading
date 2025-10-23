using Orders.Service.ViewModels;

namespace Orders.Service.DataServices.Contracts;

public interface IOrdersService
{
    Task Create(string userId, OrderCreateViewModel order);
}
