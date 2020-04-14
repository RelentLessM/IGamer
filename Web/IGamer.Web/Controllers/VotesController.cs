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
        [Route("api/[controller]")]
        public async Task<ActionResult<VotesResponseModel>> VoteOnPost(AddVoteToPostInputModel model)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            var userId = await this.userManager.GetUserIdAsync(user);

            await this.votesService.VoteOnPostAsync(model.PostId, userId, model.IsUpVote);

            var votes = await this.votesService.GetVotesOnPostAsync(model.PostId);

            var response = new VotesResponseModel() { VotesCount = votes };

            return response;
        }

        [Authorize]
        [HttpPost]
        [Route("api/[controller]/guides")]
        public async Task<ActionResult<VotesResponseModel>> VoteOnGuide(AddVoteToGuideInputModel model)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            var userId = await this.userManager.GetUserIdAsync(user);

            await this.votesService.VoteOnGuideAsync(model.GuideId, userId, model.IsUpVote);

            var votes = await this.votesService.GetVotesOnGuideAsync(model.GuideId);

            var response = new VotesResponseModel() { VotesCount = votes };

            return response;
        }
    }
}
