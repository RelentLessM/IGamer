namespace IGamer.Web.Controllers
{
    using System.Threading.Tasks;

    using Ganss.XSS;
    using IGamer.Data.Models;
    using IGamer.Services.Data.Comments;
    using IGamer.Services.Data.Replies;
    using IGamer.Web.ViewModels.Comments;
    using IGamer.Web.ViewModels.Replies;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]

    public class CommentsController : ControllerBase
    {
        private readonly ICommentsService commentsService;
        private readonly IReplyService replyService;
        private readonly UserManager<ApplicationUser> userManager;

        public CommentsController(ICommentsService commentsService, IReplyService replyService, UserManager<ApplicationUser> userManager)
        {
            this.commentsService = commentsService;
            this.replyService = replyService;
            this.userManager = userManager;
        }

        [Authorize]
        [Route("api/[controller]")]
        [HttpPost]
        public async Task<ActionResult<CommentResponseModel>> AddComment(AddCommentInputModel model)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            var userId = await this.userManager.GetUserIdAsync(user);

            model.UserId = userId;
            model.Description = new HtmlSanitizer().Sanitize(model.Description);

            if (!this.ModelState.IsValid || string.IsNullOrWhiteSpace(model.UserId) || string.IsNullOrWhiteSpace(model.Description))
            {
                return this.RedirectToAction("DetailedPost", "Posts", new { id = model.PostId });
            }

            var commentId = await this.commentsService.AddCommentToPostAsync(model);

            var response = await this.commentsService.GetCommentByIdAsync<CommentResponseModel>(commentId);

            return response;
        }

        [Authorize]
        [Route("api/reply")]
        [HttpPost]
        public async Task<ActionResult<ReplyResponseModel>> AddReply(AddReplyInputModel model)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            var userId = await this.userManager.GetUserIdAsync(user);

            model.UserId = userId;
            model.Description = new HtmlSanitizer().Sanitize(model.Description);

            if (!this.ModelState.IsValid || string.IsNullOrWhiteSpace(model.UserId) || string.IsNullOrWhiteSpace(model.Description))
            {
                return this.RedirectToAction("DetailedPost", "Posts", new { id = model.PostId });
            }

            var replyId = await this.replyService.AddReplyToPostCommentAsync(model);

            var response = await this.replyService.GetReplyByIdAsync<ReplyResponseModel>(replyId);

            return response;
        }
    }
}
