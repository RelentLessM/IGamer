namespace IGamer.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using IGamer.Services.Data.Games;
    using IGamer.Services.Data.Suggestions;
    using IGamer.Web.ViewModels.Administration.Games;
    using Microsoft.AspNetCore.Mvc;

    public class GamesController : AdministrationController
    {
        private readonly IGamesService gamesService;
        private readonly ISuggestionsService suggestionsService;

        public GamesController(
            IGamesService gamesService,
            ISuggestionsService suggestionsService)
        {
            this.gamesService = gamesService;
            this.suggestionsService = suggestionsService;
        }

        [HttpPost]
        public async Task<IActionResult> Add(int id)
        {
            var gameToAdd = await this.suggestionsService.GetById<SuggestionToGameViewModel>(id);
            if (gameToAdd == null)
            {
                this.TempData["AddInfo"] = "Something went wrong!";
                return this.RedirectToAction("All", "Suggestions");
            }

            var doesItExist = await this.gamesService.DoesGameExist(gameToAdd.Title);
            if (doesItExist)
            {
                await this.suggestionsService.DeleteSuggestionAsync(id);
                this.TempData["AddInfo"] = "Game already added!";
                return this.RedirectToAction("All", "Suggestions");
            }

            await this.gamesService.AddAsync(gameToAdd);
            await this.suggestionsService.DeleteSuggestionAsync(id);
            this.TempData["AddInfo"] = "Game added!";
            return this.RedirectToAction("All", "Suggestions");
        }
    }
}
