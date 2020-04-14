using IGamer.Services.Data.Games;
using Microsoft.AspNetCore.Authorization;

namespace IGamer.Web.Controllers
{
    using System.Threading.Tasks;

    using IGamer.Services.Data.Guides;
    using IGamer.Web.ViewModels.Guides;
    using Microsoft.AspNetCore.Mvc;

    public class GuidesController : Controller
    {
        private readonly IGuidesService guidesService;
        private readonly IGamesService gamesService;

        public GuidesController(IGuidesService guidesService, IGamesService gamesService)
        {
            this.guidesService = guidesService;
            this.gamesService = gamesService;
        }

        public async Task<IActionResult> All()
        {
            var guides = await this.guidesService.GetAllAsync<GuideViewModel>();
            var model = new AllGuidesViewModel() { Guides = guides };

            return this.View(model);
        }

        [Authorize]
        public async Task<IActionResult> Create()
        {
            var games = await this.gamesService.GetAll<GamesDropDownViewModel>();
            var model = new CreateGuideInputModel() { Games = games };
            return this.View(model);
        }
    }
}
