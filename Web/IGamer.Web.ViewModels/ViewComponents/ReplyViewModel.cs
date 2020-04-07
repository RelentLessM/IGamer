using AutoMapper;
using IGamer.Common;

namespace IGamer.Web.ViewModels.ViewComponents
{
    using System;

    using IGamer.Data.Models;
    using IGamer.Services.Mapping;

    public class ReplyViewModel : IMapFrom<ReplyOnPostComment>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public string UserUserName { get; set; }

        public string UserImageUrl { get; set; }

        public DateTime CreatedOn { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<ReplyOnPostComment, ReplyViewModel>()
                .ForMember(
                    x => x.UserImageUrl,
                    s => s.MapFrom(x => GlobalConstants.DefaultCloudinary + x.User.ImageUrl));
        }
    }
}
