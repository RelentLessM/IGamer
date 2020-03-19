namespace IGamer.Web.ViewComponents
{
    using Microsoft.AspNetCore.Mvc;

    [ViewComponent(Name = "SearchBar")]
    public class SearchBarViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return this.View();
        }
    }
}
