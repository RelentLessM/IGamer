namespace IGamer.Web.ViewComponents
{
    using System.Threading.Tasks;

    using IGamer.Services.Data.Games;
    using IGamer.Web.ViewModels.ViewComponents;
    using Microsoft.AspNetCore.Mvc;

    [ViewComponent(Name = "NewGames")]
    public class NewGamesViewComponent : ViewComponent
    {
        private readonly IGamesService gamesService;

        public NewGamesViewComponent(IGamesService gamesService)
        {
            this.gamesService = gamesService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var games = await this.gamesService.TakeNewAsync<NewGamesViewModel>();
            var result = new NewGamesListViewModel()
            {
                Games = games,
            };

            return this.View(result);
        }
    }
}
