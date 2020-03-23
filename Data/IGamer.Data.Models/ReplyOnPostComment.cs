namespace IGamer.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using IGamer.Data.Common.Models;

    public class ReplyOnPostComment : BaseDeletableModel<int>
    {
        public ReplyOnPostComment()
        {
            this.Votes = new HashSet<VoteOnPostComment>();
        }

        public int CommentId { get; set; }

        public CommentOnPost Comment { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public virtual ICollection<VoteOnPostComment> Votes { get; set; }
    }
}
