namespace IGamer.Data.Models
{
    using IGamer.Data.Common.Models;

    public class CommentOnBlog : BaseDeletableModel<int>
    {
        public string Description { get; set; }

        public int Likes { get; set; }

        public int Dislikes { get; set; }

        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public int BlogId { get; set; }

        public Blog Blog { get; set; }
    }
}