using System;
using System.Collections.Generic;
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
        public async Task SearchPostShouldTakeFive()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var context = new ApplicationDbContext(options.Options);
            await context.Users.AddAsync(new ApplicationUser() { Id = "1" });

            var postRepository = new EfDeletableEntityRepository<Post>(context);
            var guideRepository = new EfDeletableEntityRepository<Guide>(context);
            var searchBarService = new SearchBarService(postRepository, guideRepository);

            var postService = new PostService(postRepository);
            var postModels = new List<CreatePostInputModel>()
            {
               new CreatePostInputModel()
               {
                   Title = "new",
                   Category = "Action",
                   Content = "test",
                   ImageUrl = "google",
               },
               new CreatePostInputModel()
               {
                   Title = "new",
                   Category = "Action",
                   Content = "test1",
                   ImageUrl = "google",
               },
               new CreatePostInputModel()
               {
                   Title = "testttt",
                   Category = "Action",
                   Content = "new",
                   ImageUrl = "google",
               },
               new CreatePostInputModel()
               {
                   Title = "new",
                   Category = "Action",
                   Content = "test5",
                   ImageUrl = "google",
               },
               new CreatePostInputModel()
               {
                   Title = "new",
                   Category = "Action",
                   Content = "test7",
                   ImageUrl = "google",
               },
               new CreatePostInputModel()
               {
                   Title = "newtest3",
                   Category = "Action",
                   Content = "eee",
                   ImageUrl = "google",
               },
            };
            foreach (var postModel in postModels)
            {
                await postService.CreateAsync(postModel, "1");
            }

            var actual = await searchBarService.SearchPost<PostViewModel>("test", 5, 0);
            Assert.Equal(5, actual.Count());
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

            var actual = await searchBarService.SearchPost<PostViewModel>("test", 5, 0);
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

            var actual = await searchBarService.SearchPost<PostViewModel>("test", 5, 0);
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

            var actual = await searchBarService.SearchPost<PostViewModel>("test", 5, 0);
            Assert.Equal(2, actual.Count());
        }

        [Fact]
        public async Task SearchGuideShouldTakeFive()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var context = new ApplicationDbContext(options.Options);
            await context.Users.AddAsync(new ApplicationUser() { Id = "1" });
            await context.Games.AddAsync(new Game() { Id = "1" });

            var postRepository = new EfDeletableEntityRepository<Post>(context);
            var guideRepository = new EfDeletableEntityRepository<Guide>(context);
            var searchBarService = new SearchBarService(postRepository, guideRepository);

            var guideService = new GuidesService(guideRepository);
            var guideModels = new List<CreateGuideInputModel>()
            {
               new CreateGuideInputModel()
               {
                   Title = "new",
                   Category = "Action",
                   Description = "test",
                   GameId = "1",
                   ImageUrl = "google",
               },
               new CreateGuideInputModel()
               {
                   Title = "new",
                   Category = "Action",
                   Description = "test1",
                   ImageUrl = "google",
                   GameId = "1",
               },
               new CreateGuideInputModel()
               {
                   Title = "testttt",
                   Category = "Action",
                   Description = "new",
                   ImageUrl = "google",
                   GameId = "1",
               },
               new CreateGuideInputModel()
               {
                   Title = "new",
                   Category = "Action",
                   Description = "test5",
                   ImageUrl = "google",
                   GameId = "1",
               },
               new CreateGuideInputModel()
               {
                   Title = "new",
                   Category = "Action",
                   Description = "test7",
                   ImageUrl = "google",
                   GameId = "1",
               },
               new CreateGuideInputModel()
               {
                   Title = "newtest3",
                   Category = "Action",
                   Description = "eee",
                   ImageUrl = "google",
                   GameId = "1",
               },
            };
            foreach (var guideModel in guideModels)
            {
                await guideService.CreateAsync(guideModel, "1");
            }

            var actual = await searchBarService.SearchGuide<GuideViewModel>("test", 5, 0);
            Assert.Equal(5, actual.Count());
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

            var actual = await searchBarService.SearchGuide<GuideViewModel>("test", 5, 0);
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

            var actual = await searchBarService.SearchGuide<GuideViewModel>("test", 5, 0);
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

            var actual = await searchBarService.SearchGuide<GuideViewModel>("test", 5, 0);
            Assert.Equal(2, actual.Count());
        }
    }
}
