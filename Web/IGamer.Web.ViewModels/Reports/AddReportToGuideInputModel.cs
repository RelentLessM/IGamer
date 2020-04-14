namespace IGamer.Web.ViewModels.Reports
{
    using System.ComponentModel.DataAnnotations;

    using IGamer.Data.Models;
    using IGamer.Services.Mapping;

    public class AddReportToGuideInputModel : IMapTo<Report>
    {
        [Required]
        public string GuideId { get; set; }

        public string UserId { get; set; }

        [Required]
        public string Reason { get; set; }
    }
}
