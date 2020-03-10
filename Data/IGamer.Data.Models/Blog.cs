namespace IGamer.Data.Models
{
    using System;
    using System.Collections.Generic;

    using IGamer.Data.Common.Models;

    public class Blog : BaseDeletableModel<string>
    {
        public Blog()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Comments = new HashSet<CommentOnBlog>();
        }

        public string Title { get; set; }

        public string Description { get; set; }

        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public CategoryOfBlog Category { get; set; }

        public ICollection<CommentOnBlog> Comments { get; set; }

        public int Likes { get; set; }

        public int Dislikes { get; set; }
    }
}
