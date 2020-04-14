namespace IGamer.Web.ViewModels.Votes
{
    using System.ComponentModel.DataAnnotations;

    public class AddVoteToPostInputModel
    {
        [Required]
        public string PostId { get; set; }

        [Required]
        public bool IsUpVote { get; set; }
    }
}