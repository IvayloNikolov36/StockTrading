using Microsoft.EntityFrameworkCore;
using Portfolio.Service.Data;
using Portfolio.Service.Entities.Enums;
using Portfolio.Service.ViewModels;

namespace Portfolio.Service.DataServices
{
    public class UserPortfolioService : IUserPortfolioService
    {
        private readonly PortfoliosDbContext dbContext;

        public UserPortfolioService(PortfoliosDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<UserPortfolioVewModel> GetPortfolio(string userId)
        {
            UserPortfolioVewModel? user = await this.dbContext.Users.AsNoTracking()
                .Where(u => u.Id == userId)
                .Select(u => new UserPortfolioVewModel
                {
                    UserId = u.Id,
                    BoughtStocksPrice = u.Orders.Where(o => o.Side == SideEnum.Buy).Select(o => o.Price).Sum(),
                    SoldStocksPrice = u.Orders.Where(o => o.Side == SideEnum.Sell).Select(o => o.Price).Sum(),
                    StockDetails = u.Orders
                    .GroupBy(o => o.Stock.Ticker)
                    .Select(gr => new StockBasicViewModel
                    {
                        Ticker = gr.Key,
                        BoughtDetails = new StockPriceBasicViewModel
                        {
                            Count = gr.Count(x => x.Side == SideEnum.Buy),
                            TotalPrice = gr.Where(x => x.Side == SideEnum.Buy).Sum(x => x.Price)
                        },
                        SoldDetails = new StockPriceBasicViewModel
                        {
                            Count = gr.Count(x => x.Side == SideEnum.Sell),
                            TotalPrice = gr.Where(x => x.Side == SideEnum.Sell).Sum(x => x.Price)
                        }
                    })
                })
                .SingleOrDefaultAsync();

            // throw new ActionableException if user is null

            return user!;
        }
    }
}
