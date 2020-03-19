using System.Collections.Generic;

namespace IGamer.Web.ViewModels.ViewComponents
{
    public class CategoryListViewModel
    {
        public CategoryListViewModel()
        {
            this.Categories = new HashSet<CategoryViewModel>();
        }

        public ICollection<CategoryViewModel> Categories { get; set; }
    }
}
