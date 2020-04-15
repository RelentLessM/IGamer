namespace IGamer.Web.ViewComponents
{
    using System.Threading.Tasks;

    using IGamer.Services.Data.Guides;
    using IGamer.Web.ViewModels.ViewComponents;
    using Microsoft.AspNetCore.Mvc;

    [ViewComponent(Name = "RecentGuides")]
    public class RecentGuidesViewComponent : ViewComponent
    {
        private readonly IGuidesService guidesService;

        public RecentGuidesViewComponent(IGuidesService guidesService)
        {
            this.guidesService = guidesService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var guides = await this.guidesService.GetRecentAsync<RecentGuidesViewModel>();
            var model = new RecentGuidesListViewModel() { Guides = guides };

            return this.View(model);
        }
    }
}
