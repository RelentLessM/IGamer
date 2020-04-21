namespace IGamer.Web.ViewModels.Administration.Guides
{
    using System.Collections.Generic;

    public class AllGuidesForAdminViewModel
    {
        public IEnumerable<GuideForAdminViewModel> Guides { get; set; }

        public int CurrentPage { get; set; }

        public int PagesCount { get; set; }
    }
}
