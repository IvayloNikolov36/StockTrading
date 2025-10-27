using Microsoft.AspNetCore.Mvc;
using Portfolio.Service.DataServices;
using Portfolio.Service.ViewModels;

namespace Portfolio.Service.Controllers
{
    [ApiController]
    [Route("api/portfolio")]
    public class UserPortfolioController : ControllerBase
    {
        private readonly IUserPortfolioService portfolioService;

        public UserPortfolioController(IUserPortfolioService portfolioService)
        {
            this.portfolioService = portfolioService;
        }

        [HttpGet]
        [Route("{userId:Guid}")]
        public async Task<IActionResult> Get([FromRoute] Guid userId)
        {
            UserPortfolioVewModel portfolio = await this.portfolioService
                .GetPortfolio(userId.ToString());

            return this.Ok(portfolio);
        }
    }
}
