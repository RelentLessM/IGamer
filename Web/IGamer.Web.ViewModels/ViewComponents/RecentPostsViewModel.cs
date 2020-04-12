using IGamer.Data.Models;
using IGamer.Services.Mapping;

namespace IGamer.Web.ViewModels.ViewComponents
{
    using System;

    public class RecentPostsViewModel : IMapFrom<Post>
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string ShortTitle => this.Title.Length > 20
            ? this.Title.Substring(0, 20) + "..."
            : this.Title;

        public DateTime CreatedOn { get; set; }
    }
}
