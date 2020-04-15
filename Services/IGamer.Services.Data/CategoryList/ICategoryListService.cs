namespace IGamer.Services.Data.CategoryList
{
    using System.Threading.Tasks;

    using IGamer.Web.ViewModels.ViewComponents;

    public interface ICategoryListService
    {
        Task<CategoryListViewModel> TakeCategoryAsync(CategoryListViewModel model);

        Task<GuideCategoryListViewModel> TakeGuideCategoryAsync(GuideCategoryListViewModel model);
    }
}
