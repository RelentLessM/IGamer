namespace IGamer.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using IGamer.Data.Common.Models;
    using IGamer.Data.Models.Enums;

    public class VoteOnPostComment : BaseModel<int>
    {
        [Required]
        public int CommentId { get; set; }

        public virtual CommentOnPost Comment { get; set; }

        [Required]
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        [Required]
        public VoteType VoteType { get; set; }
    }
}
