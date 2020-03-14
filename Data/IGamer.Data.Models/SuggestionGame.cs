namespace IGamer.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using IGamer.Data.Common.Models;

    public class SuggestionGame : BaseDeletableModel<int>
    {
        [Required]
        public string Title { get; set; }

        [Required]
        [MinLength(50, ErrorMessage = "Description must be at least 50 characters.")]
        public string Description { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        public int Votes { get; set; }

        [Required]
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}
