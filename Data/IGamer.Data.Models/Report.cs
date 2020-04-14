namespace IGamer.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using IGamer.Data.Common.Models;

    public class Report : BaseModel<int>
    {
        [Required]
        public string Reason { get; set; }

        [Required]
        public string GuideId { get; set; }

        public virtual Guide Guide { get; set; }

        [Required]
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}
