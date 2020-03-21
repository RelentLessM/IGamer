namespace IGamer.Web.ViewModels.Posts
{
    using System;
    using System.Net;
    using System.Text.RegularExpressions;

    using IGamer.Data.Models;
    using IGamer.Services.Mapping;

    public class PostViewModel : /*IMapFrom<PostViewServiceModel>,*/ IMapFrom<Post>
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string ShortDescription
        {
            get
            {
                var content = WebUtility.HtmlDecode(Regex.Replace(this.Description, @"<[^>]*>", string.Empty));
                return content.Length > 100
                    ? content.Substring(0, 100) + "..."
                    : content;
            }
        }

        public string UserUserName { get; set; }

        public string Category { get; set; }

        public DateTime CreatedOn { get; set; }

        public int CommentsCount { get; set; }
    }
}
