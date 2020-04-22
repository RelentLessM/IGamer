namespace IGamer.Web.ViewComponents
{
    using Microsoft.AspNetCore.Mvc;

    [ViewComponent(Name = "AboutPolicy")]
    public class AboutPolicyViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return this.View();
        }
    }
}
