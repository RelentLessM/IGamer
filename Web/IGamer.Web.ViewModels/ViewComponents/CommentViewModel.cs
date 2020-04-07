namespace IGamer.Web.ViewModels.ViewComponents
{
    using System;
    using System.Collections.Generic;

    using AutoMapper;
    using IGamer.Common;
    using IGamer.Data.Models;
    using IGamer.Services.Mapping;

    public class CommentViewModel : IMapFrom<CommentOnPost>, IHaveCustomMappings
    {
        public string PostId { get; set; }

        public int Id { get; set; }

        public string Description { get; set; }

        public string UserUserName { get; set; }

        public string UserImageUrl { get; set; }

        public DateTime CreatedOn { get; set; }

        public ICollection<ReplyViewModel> Replies { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<CommentOnPost, CommentViewModel>()
                .ForMember(
                    x => x.UserImageUrl,
                    s => s.MapFrom(x => GlobalConstants.DefaultCloudinary + x.User.ImageUrl));
        }


    }
}