namespace IGamer.Web.ViewModels.Posts
{
    using System.Linq;

    using AutoMapper;
    using Ganss.XSS;
    using IGamer.Common;
    using IGamer.Data.Models;
    using IGamer.Services.Mapping;

    public class DetailedPostViewModel : IMapFrom<Post>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string SanitizedContent
            => new HtmlSanitizer().Sanitize(this.Description);

        public string UserId { get; set; }

        public string UserUserName { get; set; }

        public string UserImageUrl { get; set; }

        public string Category { get; set; }

        public int VotesCount { get; set; }

        public int CommentsCount { get; set; }

        public string ImageUrl { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Post, DetailedPostViewModel>()
                .ForMember(
                    x => x.CommentsCount,
                    s => s.MapFrom(x => x.Comments.Count + x.Comments.SelectMany(c => c.Replies).Count()))
                .ForMember(
                        x => x.UserImageUrl,
                        s => s.MapFrom(x => GlobalConstants.DefaultCloudinary + x.User.ImageUrl))
                .ForMember(
                    x => x.ImageUrl,
                    s => s.MapFrom(x => GlobalConstants.DefaultCloudinary + x.ImageUrl));
        }
    }
}
