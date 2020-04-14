namespace IGamer.Web.ViewModels.Guides
{
    using System;
    using System.Linq;
    using System.Net;
    using System.Text.RegularExpressions;

    using AutoMapper;
    using IGamer.Common;
    using IGamer.Data.Models;
    using IGamer.Services.Mapping;
    using IGamer.Web.ViewModels.Posts;

    public class GuideViewModel : IMapFrom<Guide>, IHaveCustomMappings
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

        public string GameTitle { get; set; }

        public string ImageUrl { get; set; }

        // public string GameDescription { get; set; }

        // public string GameImageUrl { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Guide, GuideViewModel>()
                .ForMember(
                    x => x.ImageUrl,
                    s => s.MapFrom(x => GlobalConstants.DefaultCloudinary + x.ImageUrl)); ;
        }
    }
}