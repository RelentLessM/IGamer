namespace IGamer.Data.Models
{
    using System;
    using System.Collections.Generic;

    using IGamer.Data.Common.Models;

    public class Guide : BaseDeletableModel<string>
    {
        public Guide()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Comments = new HashSet<CommentOnGuide>();
        }

        public string Title { get; set; }

        public string Description { get; set; }

        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public ICollection<CommentOnGuide> Comments { get; set; }

        public int Likes { get; set; }

        public int Dislikes { get; set; }

        public string ImageUrl { get; set; }

        public int CategoryId { get; set; }

        public CategoryOfGuide Category { get; set; }

        public string GameId { get; set; }

        public Game Game { get; set; }
    }
}
