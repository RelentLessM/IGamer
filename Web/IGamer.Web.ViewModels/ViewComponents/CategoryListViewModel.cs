namespace IGamer.Web.ViewModels.ViewComponents
{
    using System.Collections.Generic;

    public class CategoryListViewModel
    {
        public CategoryListViewModel()
        {
            this.Categories = new HashSet<CategoryViewModel>();
        }

        public ICollection<CategoryViewModel> Categories { get; set; }
    }
}
