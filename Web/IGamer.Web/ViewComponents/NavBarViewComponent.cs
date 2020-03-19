namespace IGamer.Web.ViewComponents
{
    using Microsoft.AspNetCore.Mvc;

    [ViewComponent(Name = "NavBar")]
    public class NavBarViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return this.View();
        }
    }
}
