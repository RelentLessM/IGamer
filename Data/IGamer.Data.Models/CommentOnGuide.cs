namespace IGamer.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using IGamer.Data.Common.Models;

    public class CommentOnGuide : BaseDeletableModel<int>
    {
        public CommentOnGuide()
        {
            this.Votes = new HashSet<VoteOnGuideComment>();
        }

        [Required]
        public string Description { get; set; }

        [Required]
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        [Required]
        public string GuideId { get; set; }

        public virtual Guide Guide { get; set; }

        public virtual ICollection<VoteOnGuideComment> Votes { get; set; }
    }
}