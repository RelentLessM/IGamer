using IGamer.Data.Models;
using IGamer.Services.Mapping;
using Microsoft.AspNetCore.Identity;

namespace IGamer.Web.Controllers
{
    using System.Threading.Tasks;

    using IGamer.Services.Data.Comments;
    using IGamer.Web.ViewModels.Comments;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/[controller]")]
    public class CommentsController : Controller
    {
        private readonly ICommentsService commentsService;
        private readonly UserManager<ApplicationUser> userManager;

        public CommentsController(ICommentsService commentsService, UserManager<ApplicationUser> userManager)
        {
            this.commentsService = commentsService;
            this.userManager = userManager;
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<CommentResponseModel>> Add(AddCommentInputModel model)
        {
            var userId = this.userManager.GetUserId(this.User);

            model.UserId = userId;

            if (!this.ModelState.IsValid || string.IsNullOrWhiteSpace(model.UserId))
            {
                return this.RedirectToAction("DetailedPost", "Posts", new { id = model.PostId });
            }

            var commentId = await this.commentsService.AddCommentAsync(model);

            var response = await this.commentsService.GetCommentByIdAsync<CommentResponseModel>(commentId);

            return response;
        }
    }
}
