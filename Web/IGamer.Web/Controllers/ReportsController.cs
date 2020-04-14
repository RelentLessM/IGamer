using System.Threading.Tasks;
using IGamer.Data.Models;
using IGamer.Services.Data.Reports;
using IGamer.Web.ViewModels.Reports;
using IGamer.Web.ViewModels.Votes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IGamer.Web.Controllers
{
    public class ReportsController : Controller
    {
        private readonly IReportsService reportsService;
        private readonly UserManager<ApplicationUser> userManager;

        public ReportsController(IReportsService reportsService, UserManager<ApplicationUser> userManager)
        {
            this.reportsService = reportsService;
            this.userManager = userManager;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> SendReport(AddReportToGuideInputModel model)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            var userId = await this.userManager.GetUserIdAsync(user);
            model.UserId = userId;
            if (!this.ModelState.IsValid || userId == null)
            {
                this.TempData["InfoMessage"] = "Something went wrong! Try again. :)";
                return this.RedirectToAction("Details", "Guides", new { id = model.GuideId });
            }

            var reportId = await this.reportsService.AddReportToGuideAsync(model);
            if (!reportId.HasValue)
            {
                this.TempData["InfoMessage"] = "You have already reported this guide. :)";
            }
            else
            {
                this.TempData["InfoMessage"] = "Your report is with us! We are going to review it soon! :)";
            }

            return this.RedirectToAction("Details", "Guides", new { id = model.GuideId });
        }
    }
}
