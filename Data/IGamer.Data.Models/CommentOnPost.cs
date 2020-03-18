using System.Collections.Generic;

namespace IGamer.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using IGamer.Data.Common.Models;

    public class CommentOnPost : BaseDeletableModel<int>
    {
        public CommentOnPost()
        {
            this.Votes = new HashSet<VoteOnPostComment>();
        }

        [Required]
        public string Description { get; set; }

        [Required]
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        [Required]
        public string PostId { get; set; }

        public virtual Post Post { get; set; }

        public virtual ICollection<VoteOnPostComment> Votes { get; set; }
    }
}