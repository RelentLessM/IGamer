namespace IGamer.Web.ViewModels.ViewComponents
{
    using System;

    using IGamer.Data.Models;
    using IGamer.Services.Mapping;

    public class ReplyViewModel : IMapFrom<ReplyOnPostComment>
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public string UserUserName { get; set; }

        public string UserImageUrl { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
