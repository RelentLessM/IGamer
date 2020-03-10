namespace IGamer.Data.Models
{
    using IGamer.Data.Common.Models;

    public class SuggestionGame : BaseDeletableModel<int>
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public int Votes { get; set; }

        public string UserId { get; set; }

        public ApplicationUser User { get; set; }
    }
}
