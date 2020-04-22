namespace IGamer.Web.ViewModels.Administration.Reports
{
    using IGamer.Data.Models;
    using IGamer.Services.Mapping;

    public class ReportForGuideViewModel : IMapFrom<Report>
    {
        public string Reason { get; set; }

        public string GuideTitle { get; set; }
    }
}
