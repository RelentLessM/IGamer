using System.Collections.Generic;

namespace IGamer.Web.ViewModels.Guides
{
    public class AllGuidesViewModel
    {
        public IEnumerable<GuideViewModel> Guides { get; set; }

        public int CurrentPage { get; set; }

        public int PagesCount { get; set; }
    }
}
