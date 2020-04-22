using IGamer.Services.Data.Reports;
using IGamer.Web.ViewModels.Administration.Reports;

namespace IGamer.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    public class ReportsController : AdministrationController
    {
        private readonly IReportsService reportsService;

        public ReportsController(IReportsService reportsService)
        {
            this.reportsService = reportsService;
        }

        public async Task<IActionResult> ByGuide(string id)
        {
            var reports = await this.reportsService.GetByGuideAsync<ReportForGuideViewModel>(id);
            var result = new AllReportsForGuideViewModel()
            {
                Reports = reports,
            };
            return this.View(result);
        }
    }
}
