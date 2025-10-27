using Portfolio.Service.ViewModels;

namespace Portfolio.Service.DataServices;

public interface IUserPortfolioService
{
    Task<UserPortfolioVewModel> GetPortfolio(string userId);
}
