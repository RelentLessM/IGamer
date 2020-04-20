namespace IGamer.Web.Areas.Administration.Controllers
{
    using IGamer.Services.Data;
    using IGamer.Web.ViewModels.Administration.Dashboard;

    using Microsoft.AspNetCore.Mvc;

    public class DashboardController : AdministrationController
    {
        public IActionResult Index()
        {
            return this.View();
        }
    }
}
