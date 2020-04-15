namespace IGamer.Web.ViewComponents
{
    using System.Threading.Tasks;

    using IGamer.Services.Data.CategoryList;
    using IGamer.Web.ViewModels.ViewComponents;
    using Microsoft.AspNetCore.Mvc;

    [ViewComponent(Name = "GuideCategoryList")]
    public class GuideCategoryListViewComponent : ViewComponent
    {
        private readonly ICategoryListService categoryListService;

        public GuideCategoryListViewComponent(ICategoryListService categoryListService)
        {
            this.categoryListService = categoryListService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = new GuideCategoryListViewModel();
            var categories = await this.categoryListService.TakeGuideCategoryAsync(model);

            return this.View(categories);
        }
    }
}
