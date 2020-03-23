namespace IGamer.Web.ViewComponents
{
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

        public IViewComponentResult Invoke(string id)
        {
            var comments = this.commentsService.GetAll<CommentViewModel>(id);
            var commentArea = new CommentSectionViewModel() { Comments = comments };

            return this.View(commentArea);
        }
    }
}
