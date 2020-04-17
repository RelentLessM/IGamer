namespace IGamer.Web.ViewModels.Suggestions
{
    using System.ComponentModel.DataAnnotations;

    using IGamer.Data.Models;
    using IGamer.Services.Mapping;

    public class CreateSuggestionInputModel : IMapTo<SuggestionGame>
    {
        [Required]
        public string Title { get; set; }

        [Required]
        [MinLength(50, ErrorMessage = "Description must be at least 50 characters.")]
        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public string UserId { get; set; }
    }
}
