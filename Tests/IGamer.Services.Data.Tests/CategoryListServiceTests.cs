namespace IGamer.Services.Data.Tests
{
    using System;
    using System.Reflection;
    using System.Threading.Tasks;

    using IGamer.Data;
    using IGamer.Data.Models;
    using IGamer.Data.Models.Enums;
    using IGamer.Data.Repositories;
    using IGamer.Services.Data.CategoryList;
    using IGamer.Services.Mapping;
    using IGamer.Web.ViewModels.ViewComponents;
    using Microsoft.EntityFrameworkCore;
    using Xunit;

    public class CategoryListServiceTests
    {
        public CategoryListServiceTests()
        {
            AutoMapperConfig
                .RegisterMappings(typeof(CategoryListViewModel).GetTypeInfo().Assembly);
        }

        [Fact]
        public async Task TakeCategoryShouldTakeAllPostCategories()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var context = new ApplicationDbContext(options.Options);

            var postRepository = new EfDeletableEntityRepository<Post>(context);
            var guideRepository = new EfDeletableEntityRepository<Guide>(context);
            var service = new CategoryListService(postRepository, guideRepository);
            var model = new CategoryListViewModel();

            var actual = await service.TakeCategoryAsync(model);
            var expected = Enum.GetNames(typeof(CategoryOfPost)).Length;
            Assert.Equal(expected, actual.Categories.Count);
        }

        [Fact]
        public async Task TakeGuideCategoryShouldTakeAllGuideCategories()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var context = new ApplicationDbContext(options.Options);

            var postRepository = new EfDeletableEntityRepository<Post>(context);
            var guideRepository = new EfDeletableEntityRepository<Guide>(context);
            var service = new CategoryListService(postRepository, guideRepository);
            var model = new GuideCategoryListViewModel();

            var actual = await service.TakeGuideCategoryAsync(model);
            var expected = Enum.GetNames(typeof(CategoryOfGuide)).Length;
            Assert.Equal(expected, actual.Categories.Count);
        }
    }
}
