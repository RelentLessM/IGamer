namespace IGamer.Web.ViewModels.Administration.Games
{
    using System.ComponentModel.DataAnnotations;

    using IGamer.Data.Models;
    using IGamer.Services.Mapping;

    public class SuggestionToGameViewModel : IMapFrom<SuggestionGame>, IMapTo<Game>
    {
        [Required]
        public string Title { get; set; }

        [Required]
        [MinLength(50, ErrorMessage = "Description must be at least 50 characters.")]
        public string Description { get; set; }

        [Required]
        public string ImageUrl { get; set; }
    }
}
