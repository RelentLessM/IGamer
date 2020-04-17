namespace IGamer.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using IGamer.Data.Common.Models;

    public class SuggestionGame : BaseDeletableModel<int>
    {
        public SuggestionGame()
        {
            this.Votes = new HashSet<VoteOnSuggestionGame>();
        }

        [Required]
        public string Title { get; set; }

        [Required]
        [MinLength(50, ErrorMessage = "Description must be at least 50 characters.")]
        public string Description { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        [Required]
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public virtual ICollection<VoteOnSuggestionGame> Votes{ get; set; }
    }
}
