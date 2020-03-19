namespace IGamer.Services.Data.ServiceModels
{
    using System;

    using IGamer.Data.Models;
    using IGamer.Services.Mapping;

    public class PostViewServiceModel : IMapFrom<Post>
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public string UserUserName { get; set; }

        public string Category { get; set; }

        public DateTime CreatedOn { get; set; }

        public int CommentsCount { get; set; }
    }
}
