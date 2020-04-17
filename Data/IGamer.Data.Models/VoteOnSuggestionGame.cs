namespace IGamer.Data.Models
{
    using IGamer.Data.Common.Models;

    public class VoteOnSuggestionGame : BaseModel<int>
    {
        public int SuggestionGameId { get; set; }

        public virtual SuggestionGame SuggestionGame { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}
