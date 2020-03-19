namespace IGamer.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using IGamer.Data.Common.Models;
    using IGamer.Data.Models.Enums;

    public class Post : BaseDeletableModel<string>
    {
        public Post()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Comments = new HashSet<CommentOnPost>();
            this.Votes = new HashSet<VoteOnPost>();
        }

        [Required]
        [StringLength(20, ErrorMessage = "The title must be between {2} and {1} characters.", MinimumLength = 6)]
        public string Title { get; set; }

        [Required]
        [MinLength(20, ErrorMessage = "Content must be more than 20 characters.")]
        public string Description { get; set; }

        [Required]
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        [Required]
        public virtual CategoryOfPost Category { get; set; }

        public virtual ICollection<CommentOnPost> Comments { get; set; }

        public virtual ICollection<VoteOnPost> Votes { get; set; }
    }
}
