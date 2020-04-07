namespace IGamer.Web.ViewModels.Comments
{
    using System;
    using System.Collections.Generic;

    using AutoMapper;
    using IGamer.Common;
    using IGamer.Data.Models;
    using IGamer.Services.Mapping;
    using IGamer.Web.ViewModels.ViewComponents;

    public class CommentResponseModel : IMapFrom<CommentOnPost>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public string UserUserName { get; set; }

        public string UserImageUrl { get; set; }

        public DateTime CreatedOn { get; set; }

        public string PostId { get; set; }

        public ICollection<ReplyViewModel> Replies { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<CommentOnPost, CommentResponseModel>()
                .ForMember(
                    x => x.UserImageUrl,
                    s => s.MapFrom(x => GlobalConstants.DefaultCloudinary + x.User.ImageUrl));
        }
    }
}