using System.Collections.Generic;

namespace IGamer.Web.ViewModels.ViewComponents
{
    public class GuideCategoryListViewModel
    {
        public GuideCategoryListViewModel()
        {
            this.Categories = new HashSet<GuideCategoryViewModel>();
        }

        public ICollection<GuideCategoryViewModel> Categories { get; set; }
    }
}
