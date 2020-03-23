namespace IGamer.Web.Controllers
{
    using System.Threading.Tasks;

    using IGamer.Data.Models;
    using IGamer.Services.Data.Votes;
    using IGamer.Web.ViewModels.Votes;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/[controller]")]
    public class VotesController : ControllerBase
    {
        private readonly IVotesService votesService;
        private readonly UserManager<ApplicationUser> userManager;

        public VotesController(IVotesService votesService, UserManager<ApplicationUser> userManager)
        {
            this.votesService = votesService;
            this.userManager = userManager;
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<VotesResponseModel>> Vote(AddVoteInputModel model)
        {
            var userId = this.userManager.GetUserId(this.User);

            await this.votesService.VoteOnPostAsync(model.PostId, userId, model.IsUpVote);

            var votes = await this.votesService.GetVotesAsync(model.PostId);

            var response = new VotesResponseModel() { VotesCount = votes };

            return response;
        }
    }
}
