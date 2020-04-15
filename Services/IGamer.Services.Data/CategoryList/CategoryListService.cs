namespace IGamer.Services.Data.CategoryList
{
    using System;
    using System.Threading.Tasks;

    using IGamer.Data.Common.Repositories;
    using IGamer.Data.Models;
    using IGamer.Data.Models.Enums;
    using IGamer.Web.ViewModels.ViewComponents;
    using Microsoft.EntityFrameworkCore;

    public class CategoryListService : ICategoryListService
    {
        private readonly IDeletableEntityRepository<Post> postsRepository;
        private readonly IDeletableEntityRepository<Guide> guideRepository;

        public CategoryListService(IDeletableEntityRepository<Post> postsRepository, IDeletableEntityRepository<Guide> guideRepository)
        {
            this.postsRepository = postsRepository;
            this.guideRepository = guideRepository;
        }

        public async Task<CategoryListViewModel> TakeCategoryAsync(CategoryListViewModel model)
        {
            foreach (var category in (CategoryOfPost[])Enum.GetValues(typeof(CategoryOfPost)))
            {
                var postsCount = await this.postsRepository.All().CountAsync(x => x.Category == category);
                var categoryToAdd = new CategoryViewModel()
                {
                    CategoryName = category.ToString(),
                    PostsCount = postsCount,
                };
                model.Categories.Add(categoryToAdd);
            }

            return model;
        }

        public async Task<GuideCategoryListViewModel> TakeGuideCategoryAsync(GuideCategoryListViewModel model)
        {
            foreach (var category in (CategoryOfGuide[])Enum.GetValues(typeof(CategoryOfGuide)))
            {
                var guidesCount = await this.guideRepository.All().CountAsync(x => x.Category == category);
                var categoryToAdd = new GuideCategoryViewModel()
                {
                    CategoryName = category.ToString(),
                    GuidesCount = guidesCount,
                };
                model.Categories.Add(categoryToAdd);
            }

            return model;
        }
    }
}
