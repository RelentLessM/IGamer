namespace IGamer.Web.ViewModels.Votes
{
    using System.ComponentModel.DataAnnotations;

    public class AddVoteToGuideInputModel
    {
        [Required]
        public string GuideId { get; set; }

        [Required]
        public bool IsUpVote { get; set; }
    }
}
