namespace IGamer.Web.ViewModels.Replies
{
    using System;

    using IGamer.Data.Models;
    using IGamer.Services.Mapping;

    public class ReplyResponseModel : IMapFrom<ReplyOnPostComment>
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public string UserUserName { get; set; }

        public string UserImageUrl { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}