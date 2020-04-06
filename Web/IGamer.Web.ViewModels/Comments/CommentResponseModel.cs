namespace IGamer.Web.ViewModels.Comments
{
    using System;
    using System.Collections.Generic;

    using IGamer.Data.Models;
    using IGamer.Services.Mapping;
    using IGamer.Web.ViewModels.ViewComponents;

    public class CommentResponseModel: IMapFrom<CommentOnPost>
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public string UserUserName { get; set; }

        public string UserImageUrl { get; set; }

        public DateTime CreatedOn { get; set; }

        public string PostId { get; set; }

        public ICollection<ReplyViewModel> Replies { get; set; }
    }
}