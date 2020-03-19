using System;
using System.Linq;
using System.Threading.Tasks;
using IGamer.Data.Common.Repositories;
using IGamer.Data.Models;
using IGamer.Data.Models.Enums;
using IGamer.Web.ViewModels.ViewComponents;
using Microsoft.EntityFrameworkCore;

namespace IGamer.Services.Data.CategoryList
{
    public class CategoryListService : ICategoryListService
    {
        private readonly IDeletableEntityRepository<Post> repository;

        public CategoryListService(IDeletableEntityRepository<Post> repository)
        {
            this.repository = repository;
        }

        public async Task<CategoryListViewModel> TakeCategoryAsync(CategoryListViewModel model)
        {
            foreach (var category in (CategoryOfPost[])Enum.GetValues(typeof(CategoryOfPost)))
            {
                var postsCount = await this.repository.All().CountAsync(x => x.Category == category);
                var categoryToAdd = new CategoryViewModel()
                {
                    CategoryName = category.ToString(),
                    PostsCount = postsCount,
                };
                model.Categories.Add(categoryToAdd);
            }

            return model;
        }
    }
}
