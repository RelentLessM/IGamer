namespace IGamer.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using IGamer.Data.Common.Models;

    public class CommentOnGuide : BaseDeletableModel<int>
    {
        [Required]
        public string Description { get; set; }

        public int Likes { get; set; }

        public int Dislikes { get; set; }

        [Required]
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        [Required]
        public string GuideId { get; set; }

        public virtual Guide Guide { get; set; }
    }
}