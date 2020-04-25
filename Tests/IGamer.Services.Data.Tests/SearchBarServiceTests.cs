using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using IGamer.Data;
using IGamer.Data.Models;
using IGamer.Data.Repositories;
using IGamer.Services.Data.Guides;
using IGamer.Services.Data.Posts;
using IGamer.Services.Data.SearchBar;
using IGamer.Services.Mapping;
using IGamer.Web.ViewModels.Guides;
using IGamer.Web.ViewModels.Posts;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace IGamer.Services.Data.Tests
{
    public class SearchBarServiceTests
    {
        public SearchBarServiceTests()
        {
            AutoMapperConfig
                .RegisterMappings(typeof(PostViewModel).GetTypeInfo().Assembly);
        }

        [Fact]
        public async Task SearchPostShouldFindOneByContent()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var context = new ApplicationDbContext(options.Options);
            await context.Users.AddAsync(new ApplicationUser() { Id = "1" });

            var postRepository = new EfDeletableEntityRepository<Post>(context);
            var guideRepository = new EfDeletableEntityRepository<Guide>(context);
            var searchBarService = new SearchBarService(postRepository, guideRepository);

            var postService = new PostService(postRepository);
            var postModel = new CreatePostInputModel()
            {
                Title = "new",
                Category = "Action",
                Content = "test",
                ImageUrl = "google",
            };

            await postService.CreateAsync(postModel, "1");

            var actual = await searchBarService.SearchPost<PostViewModel>("test");
            Assert.Single(actual);
        }

        [Fact]
        public async Task SearchPostShouldFindOneByTitle()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var context = new ApplicationDbContext(options.Options);
            await context.Users.AddAsync(new ApplicationUser() { Id = "1" });

            var postRepository = new EfDeletableEntityRepository<Post>(context);
            var guideRepository = new EfDeletableEntityRepository<Guide>(context);
            var searchBarService = new SearchBarService(postRepository, guideRepository);

            var postService = new PostService(postRepository);
            var postModel = new CreatePostInputModel()
            {
                Title = "testings",
                Category = "Action",
                Content = "new",
                ImageUrl = "google",
            };

            await postService.CreateAsync(postModel, "1");

            var actual = await searchBarService.SearchPost<PostViewModel>("test");
            Assert.Single(actual);
        }

        [Fact]
        public async Task SearchPostShouldFindOneByTitleAndOneByContent()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var context = new ApplicationDbContext(options.Options);
            await context.Users.AddAsync(new ApplicationUser() { Id = "1" });

            var postRepository = new EfDeletableEntityRepository<Post>(context);
            var guideRepository = new EfDeletableEntityRepository<Guide>(context);
            var searchBarService = new SearchBarService(postRepository, guideRepository);

            var postService = new PostService(postRepository);
            var postModel = new CreatePostInputModel()
            {
                Title = "testings",
                Category = "Action",
                Content = "new",
                ImageUrl = "google",
            };

            await postService.CreateAsync(postModel, "1");

            var secondModel = new CreatePostInputModel()
            {
                Title = "newww",
                Category = "Action",
                Content = "testttt",
                ImageUrl = "google",
            };

            await postService.CreateAsync(postModel, "1");

            var actual = await searchBarService.SearchPost<PostViewModel>("test");
            Assert.Equal(2, actual.Count());
        }

        [Fact]
        public async Task SearchGuideShouldFindOneByContent()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var context = new ApplicationDbContext(options.Options);
            await context.Users.AddAsync(new ApplicationUser() { Id = "2" });
            await context.Games.AddAsync(new Game() { Id = "1" });

            var postRepository = new EfDeletableEntityRepository<Post>(context);
            var guideRepository = new EfDeletableEntityRepository<Guide>(context);
            var searchBarService = new SearchBarService(postRepository, guideRepository);

            var guideService = new GuidesService(guideRepository);
            var guideModel = new CreateGuideInputModel()
            {
                Title = "newww",
                Category = "Action",
                Description = "test",
                GameId = "1",
                ImageUrl = "test",
            };

            await guideService.CreateAsync(guideModel, "2");

            var actual = await searchBarService.SearchGuide<GuideViewModel>("test");
            Assert.Single(actual);
        }

        [Fact]
        public async Task SearchGuideShouldFindOneByTitle()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var context = new ApplicationDbContext(options.Options);
            await context.Users.AddAsync(new ApplicationUser() { Id = "2" });
            await context.Games.AddAsync(new Game() { Id = "1" });

            var postRepository = new EfDeletableEntityRepository<Post>(context);
            var guideRepository = new EfDeletableEntityRepository<Guide>(context);
            var searchBarService = new SearchBarService(postRepository, guideRepository);

            var guideService = new GuidesService(guideRepository);
            var guideModel = new CreateGuideInputModel()
            {
                Title = "testtesttest",
                Category = "Action",
                Description = "sssss",
                GameId = "1",
                ImageUrl = "test",
            };

            await guideService.CreateAsync(guideModel, "2");

            var actual = await searchBarService.SearchGuide<GuideViewModel>("test");
            Assert.Single(actual);
        }

        [Fact]
        public async Task SearchGuideShouldFindOneByTitleAndOneByContent()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var context = new ApplicationDbContext(options.Options);
            await context.Users.AddAsync(new ApplicationUser() { Id = "2" });
            await context.Games.AddAsync(new Game() { Id = "1" });

            var postRepository = new EfDeletableEntityRepository<Post>(context);
            var guideRepository = new EfDeletableEntityRepository<Guide>(context);
            var searchBarService = new SearchBarService(postRepository, guideRepository);

            var guideService = new GuidesService(guideRepository);
            var guideModel = new CreateGuideInputModel()
            {
                Title = "testtesttest",
                Category = "Action",
                Description = "sssss",
                GameId = "1",
                ImageUrl = "test",
            };

            await guideService.CreateAsync(guideModel, "2");
            var newGuideModel = new CreateGuideInputModel()
            {
                Title = "newwww",
                Category = "Action",
                Description = "tesestest",
                GameId = "1",
                ImageUrl = "test",
            };

            await guideService.CreateAsync(newGuideModel, "2");

            var actual = await searchBarService.SearchGuide<GuideViewModel>("test");
            Assert.Equal(2, actual.Count());
        }
    }
}
