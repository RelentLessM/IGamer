namespace IGamer.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using IGamer.Data.Common.Models;
    using IGamer.Data.Models.Enums;

    public class VoteOnPost : BaseModel<int>
    {
        [Required]
        public string PostId { get; set; }

        public virtual Post Post { get; set; }

        [Required]
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        [Required]
        public VoteType VoteType { get; set; }
    }
}
