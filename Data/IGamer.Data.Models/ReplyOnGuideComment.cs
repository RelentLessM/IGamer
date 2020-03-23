namespace IGamer.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using IGamer.Data.Common.Models;

    public class ReplyOnGuideComment: BaseDeletableModel<int>
    {
        public ReplyOnGuideComment()
        {
            this.Votes = new HashSet<VoteOnGuideComment>();
        }

        public int CommentId { get; set; }

        public CommentOnGuide Comment { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public virtual ICollection<VoteOnGuideComment> Votes { get; set; }
    }
}
