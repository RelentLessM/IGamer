namespace IGamer.Web.Controllers
{
    using System.Threading.Tasks;

    using IGamer.Services.Data.Guides;
    using IGamer.Web.ViewModels.Guides;
    using Microsoft.AspNetCore.Mvc;

    public class GuidesController : Controller
    {
        private readonly IGuidesService guidesService;

        public GuidesController(IGuidesService guidesService)
        {
            this.guidesService = guidesService;
        }

        public async Task<IActionResult> All()
        {
            var guides = await this.guidesService.GetAll<GuideViewModel>();
            var model = new AllGuidesViewModel() { Guides = guides };

            return this.View(model);
        }
    }
}
