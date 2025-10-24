using Orders.Service.ViewModels;

namespace Orders.Service.DataServices.Contracts;

public interface IOrdersService
{
    Task<OrderCreateResultViewModel> Create(string userId, OrderCreateViewModel order);
}
