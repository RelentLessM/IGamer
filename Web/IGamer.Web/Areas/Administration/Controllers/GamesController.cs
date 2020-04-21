namespace IGamer.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    public class GamesController : AdministrationController
    {
        public async Task<IActionResult> Add(string id)
        {
            return this.Ok();
        }
    }
}
