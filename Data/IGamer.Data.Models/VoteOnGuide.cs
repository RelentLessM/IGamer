namespace IGamer.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using IGamer.Data.Common.Models;
    using IGamer.Data.Models.Enums;

    public class VoteOnGuide : BaseModel<int>
    {
        [Required]
        public string GuideId { get; set; }

        public virtual Guide Guide { get; set; }

        [Required]
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        [Required]
        public VoteType VoteType { get; set; }
    }
}
