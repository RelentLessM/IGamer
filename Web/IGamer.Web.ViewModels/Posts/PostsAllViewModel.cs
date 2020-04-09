namespace IGamer.Web.ViewModels.Posts
{
    using System.Collections.Generic;

    public class PostsAllViewModel
    {
        public IEnumerable<PostViewModel> Posts { get; set; }

        public int CurrentPage { get; set; }

        public int PagesCount { get; set; }
    }
}
