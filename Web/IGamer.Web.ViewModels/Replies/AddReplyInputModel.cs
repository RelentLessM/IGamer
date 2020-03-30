namespace IGamer.Web.ViewModels.Replies
{
    using System.ComponentModel.DataAnnotations;

    using IGamer.Data.Models;
    using IGamer.Services.Mapping;

    public class AddReplyInputModel : IMapTo<ReplyOnPostComment>
    {
        [Required]
        public string PostId { get; set; }

        [Required]
        public string Description { get; set; }

        public string UserId { get; set; }

        [Required]
        public int CommentId { get; set; }
    }
}