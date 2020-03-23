namespace IGamer.Web.ViewModels.Comments
{
    using System.ComponentModel.DataAnnotations;

    using IGamer.Data.Models;
    using IGamer.Services.Mapping;

    public class AddCommentInputModel : IMapTo<CommentOnPost>
    {
        [Required]
        public string Description { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public string PostId { get; set; }
    }
}
