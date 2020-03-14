namespace IGamer.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using IGamer.Data.Common.Models;

    public class Game : BaseModel<string>
    {
        public Game()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Guides = new HashSet<Guide>();
        }

        [Required]
        public string Title { get; set; }

        [Required]
        [MinLength(50, ErrorMessage = "Description must be at least 50 characters.")]
        public string Description { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        public virtual ICollection<Guide> Guides { get; set; }
    }
}