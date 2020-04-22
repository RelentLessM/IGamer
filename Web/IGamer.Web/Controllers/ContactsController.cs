using System.Threading.Tasks;
using IGamer.Common;
using IGamer.Services.Messaging;
using IGamer.Web.ViewModels.Contacts;
using Microsoft.AspNetCore.Mvc;

namespace IGamer.Web.Controllers
{
    public class ContactsController : Controller
    {
        private readonly IEmailSender emailSender;

        public ContactsController(IEmailSender emailSender)
        {
            this.emailSender = emailSender;
        }

        public IActionResult Index()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(ContactFormInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            await this.emailSender
                .SendEmailAsync(model.Email, model.Name, GlobalConstants.ContactEmail, model.Subject, model.Message);
            this.TempData["SendMessage"] =
                "Your message was sent and we will review it as soon as possible! Thank you for your patience. :)";

            return this.RedirectToAction("Index", "Home");
        }
    }
}
