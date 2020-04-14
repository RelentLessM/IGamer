using AutoMapper;
using IGamer.Common;

namespace IGamer.Web.ViewModels.Guides
{
    using Ganss.XSS;
    using IGamer.Data.Models;
    using IGamer.Services.Mapping;

    public class DetailedGuideViewModel : IMapFrom<Guide>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string ImageUrl { get; set; }

        public string Title { get; set; }

        public string Category { get; set; }

        public string Description { get; set; }

        public string SanitizedContent
            => new HtmlSanitizer().Sanitize(this.Description);

        public int VotesCount { get; set; }

        public string UserUserName { get; set; }

        public string UserImageUrl { get; set; }

        public string GameTitle { get; set; }

        public string GameImageUrl { get; set; }

        public string GameDescription { get; set; }

        public string ShortGameDescription => this.GameDescription.Length > 30
            ? ShortGameDescription.Substring(0, 30) + "..."
            : ShortGameDescription;

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Guide, DetailedGuideViewModel>()
                .ForMember(
                    x => x.UserImageUrl,
                    s => s.MapFrom(x => GlobalConstants.DefaultCloudinary + x.User.ImageUrl))
                .ForMember(
                    x => x.ImageUrl,
                    s => s.MapFrom(x => GlobalConstants.DefaultCloudinary + x.ImageUrl))
                .ForMember(
                x => x.GameImageUrl,
                s => s.MapFrom(x => GlobalConstants.DefaultCloudinary + x.Game.ImageUrl));
        }
    }
}
