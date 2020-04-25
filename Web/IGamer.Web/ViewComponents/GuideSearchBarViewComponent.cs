namespace IGamer.Web.ViewComponents
{
    using Microsoft.AspNetCore.Mvc;

    [ViewComponent(Name = "GuideSearchBar")]
    public class GuideSearchBarViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return this.View();
        }
    }
}
