namespace IGamer.Web.ViewComponents
{
    using System.Threading.Tasks;

    using IGamer.Services.Data.CategoryList;
    using IGamer.Web.ViewModels.ViewComponents;
    using Microsoft.AspNetCore.Mvc;

    [ViewComponent(Name = "CategoryList")]
    public class CategoryListViewComponent : ViewComponent
    {
        private readonly ICategoryListService categoryListService;

        public CategoryListViewComponent(ICategoryListService categoryListService)
        {
            this.categoryListService = categoryListService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = new CategoryListViewModel();
            var categories = await this.categoryListService.TakeCategoryAsync(model);

            return this.View(categories);
        }
    }
}
