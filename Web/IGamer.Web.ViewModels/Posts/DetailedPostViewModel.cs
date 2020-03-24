﻿namespace IGamer.Web.ViewModels.Posts
{
    using Ganss.XSS;
    using IGamer.Data.Models;
    using IGamer.Services.Mapping;

    public class DetailedPostViewModel : IMapFrom<Post>
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string SanitizedContent 
            => new HtmlSanitizer().Sanitize(this.Description);

        public string UserUserName { get; set; }

        public string UserImageUrl { get; set; }

        public string Category { get; set; }

        public int VotesCount { get; set; }

        public int CommentsCount { get; set; }
    }
}