namespace IGamer.Data.Models
{
    using IGamer.Data.Common.Models;

    public class CommentOnGuide : BaseDeletableModel<int>
    {
        public string Description { get; set; }

        public int Likes { get; set; }

        public int Dislikes { get; set; }

        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public string GuideId { get; set; }

        public Guide Guide { get; set; }
    }
}