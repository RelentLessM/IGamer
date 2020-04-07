namespace IGamer.Web.ViewComponents
{
    using System.Threading.Tasks;

    using IGamer.Services.Data.Comments;
    using IGamer.Web.ViewModels.ViewComponents;
    using Microsoft.AspNetCore.Mvc;

    [ViewComponent(Name = "CommentSection")]
    public class CommentSectionViewComponent : ViewComponent
    {
        private readonly ICommentsService commentsService;

        public CommentSectionViewComponent(ICommentsService commentsService)
        {
            this.commentsService = commentsService;
        }

        public async Task<IViewComponentResult> InvokeAsync(string id)
        {
            var comments = await this.commentsService.GetAllAsync<CommentViewModel>(id);
            var commentArea = new CommentSectionViewModel() { Comments = comments };

            return this.View(commentArea);
        }
    }
}
