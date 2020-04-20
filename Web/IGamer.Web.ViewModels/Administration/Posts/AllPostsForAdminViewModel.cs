namespace IGamer.Web.ViewModels.Administration.Posts
{
    using System.Collections.Generic;

    public class AllPostsForAdminViewModel
    {
        public IEnumerable<PostForAdminViewModel> Posts { get; set; }

        public int CurrentPage { get; set; }

        public int PagesCount { get; set; }
    }
}
