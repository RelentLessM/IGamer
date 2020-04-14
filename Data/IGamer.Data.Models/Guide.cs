namespace IGamer.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using IGamer.Data.Common.Models;
    using IGamer.Data.Models.Enums;

    public class Guide : BaseDeletableModel<string>
    {
        public Guide()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Reports = new HashSet<Report>();
            this.Votes = new HashSet<VoteOnGuide>();
        }

        [Required]
        [StringLength(100, ErrorMessage = "{0} must be between {2} and {1} characters.", MinimumLength = 6)]
        public string Title { get; set; }

        [Required]
        [MinLength(400, ErrorMessage = "Content must be at least 400 symbols.")]
        public string Description { get; set; }

        [Required]
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }


        [Required]
        public string ImageUrl { get; set; }

        [Required]
        public virtual CategoryOfGuide Category { get; set; }

        [Required]
        public string GameId { get; set; }

        public virtual Game Game { get; set; }

        public virtual ICollection<VoteOnGuide> Votes { get; set; }

        public virtual ICollection<Report> Reports { get; set; }
    }
}
