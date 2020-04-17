namespace IGamer.Web.ViewModels.Votes
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using IGamer.Web.ViewModels.Suggestions;

    public class AddVoteToSuggestionGameInputModel
    {
        [Range(1, int.MaxValue)]
        [Required(ErrorMessage = "Game is required!")]
        [Display(Name = "Game")]
        public int SuggestionGameId { get; set; }

        public IEnumerable<SuggestionForDropDownViewModel> SuggestionGames { get; set; }

        public string UserId { get; set; }
    }
}
