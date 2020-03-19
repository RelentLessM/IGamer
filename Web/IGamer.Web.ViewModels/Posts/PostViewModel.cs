using IGamer.Data.Models;

namespace IGamer.Web.ViewModels.Posts
{
    using System;

    using IGamer.Services.Data.ServiceModels;
    using IGamer.Services.Mapping;

    public class PostViewModel : /*IMapFrom<PostViewServiceModel>,*/ IMapFrom<Post>
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public string UserUserName { get; set; }

        public string Category { get; set; }

        public DateTime CreatedOn { get; set; }

        public int CommentsCount { get; set; }
    }
}
