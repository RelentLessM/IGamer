namespace IGamer.Web.ViewComponents
{
    using System.Threading.Tasks;

    using IGamer.Services.Data.Guides;
    using IGamer.Web.ViewModels.ViewComponents;
    using Microsoft.AspNetCore.Mvc;

    [ViewComponent(Name = "LatestGuide")]
    public class LatestGuideViewComponent : ViewComponent
    {
        private readonly IGuidesService guidesService;

        public LatestGuideViewComponent(IGuidesService guidesService)
        {
            this.guidesService = guidesService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var guide = await this.guidesService.TakeLatestGuideAsync<LatestGuideViewModel>();

            return this.View(guide);
        }
    }
}
