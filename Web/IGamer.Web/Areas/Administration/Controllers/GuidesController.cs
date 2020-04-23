using System;
using System.Threading.Tasks;
using IGamer.Common;
using IGamer.Services.Data.Guides;
using IGamer.Web.ViewModels.Administration.Guides;
using IGamer.Web.ViewModels.Administration.Posts;
using Microsoft.AspNetCore.Mvc;

namespace IGamer.Web.Areas.Administration.Controllers
{
    public class GuidesController : AdministrationController
    {
        private readonly IGuidesService guidesService;

        public GuidesController(IGuidesService guidesService)
        {
            this.guidesService = guidesService;
        }

        public async Task<IActionResult> AllGuides(int page = 1)
        {
            var guidesCount = await this.guidesService.GetAllCountAsync();
            var pagesCount = (int)Math.Ceiling((double)guidesCount / GlobalConstants.ItemsPerPage);
            if (page > pagesCount)
            {
                page = pagesCount;
            }

            if (page <= 0)
            {
                page = 1;
            }

            var guides = await this.guidesService
                .GetAllForAdminAsync<GuideForAdminViewModel>(GlobalConstants.ItemsPerPage, (page - 1) * GlobalConstants.ItemsPerPage);
            var result = new AllGuidesForAdminViewModel()
            {
                Guides = guides,
                PagesCount = (int)Math.Ceiling((double)guidesCount / GlobalConstants.ItemsPerPage),
            };

            if (result.PagesCount == 0)
            {
                result.PagesCount = 1;
            }

            if (page > result.PagesCount)
            {
                page = result.PagesCount;
            }

            result.CurrentPage = page;

            return this.View(result);
        }
    }
}
