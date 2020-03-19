using System.Threading.Tasks;
using IGamer.Web.ViewModels.ViewComponents;

namespace IGamer.Services.Data.CategoryList
{
    public interface ICategoryListService
    {
        Task<CategoryListViewModel> TakeCategoryAsync(CategoryListViewModel model);
    }
}
