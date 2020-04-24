namespace IGamer.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;

    using IGamer.Data;
    using IGamer.Data.Models;
    using IGamer.Data.Models.Enums;
    using IGamer.Data.Repositories;
    using IGamer.Services.Data.Posts;
    using IGamer.Services.Mapping;
    using IGamer.Web.ViewModels.Posts;
    using Microsoft.EntityFrameworkCore;
    using Xunit;

    public class PostsServiceTests
    {
        public PostsServiceTests()
        {
            AutoMapperConfig
                .RegisterMappings(typeof(CreatePostInputModel).GetTypeInfo().Assembly);
        }

        [Fact]
        public async Task AddOnPostShouldAdd()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var context = new ApplicationDbContext(options.Options);

            var repository = new EfDeletableEntityRepository<Post>(context);
            var service = new PostService(repository);
            var model = new CreatePostInputModel()
            {
                Title = "new",
                Category = "Action",
                Content = "test",
                ImageUrl = "google",
            };

            await service.CreateAsync(model, "1");
            var actual = await service.GetAllCountAsync();
            Assert.Equal(1, actual);
        }

        [Fact]
        public async Task GetAllShouldTakeThree()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var context = new ApplicationDbContext(options.Options);
            await context.Users.AddAsync(new ApplicationUser() { Id = "1" });

            var repository = new EfDeletableEntityRepository<Post>(context);
            var service = new PostService(repository);
            var models = new List<CreatePostInputModel>()
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
                   Title = "new2",
                   Category = "Action",
                   Content = "test2",
                   ImageUrl = "google2",
               },
               new CreatePostInputModel()
               {
                   Title = "new3",
                   Category = "Action",
                   Content = "test3",
                   ImageUrl = "google3",
               },
            };
            foreach (var model in models)
            {
                await service.CreateAsync(model, "1");
            }

            var actual = await service.GetAllAsync<PostViewModel>(5, 0);
            Assert.Equal(3, actual.Count());
        }

        [Fact]
        public async Task GetAllShouldTakeFive()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var context = new ApplicationDbContext(options.Options);
            await context.Users.AddAsync(new ApplicationUser() { Id = "1" });

            var repository = new EfDeletableEntityRepository<Post>(context);
            var service = new PostService(repository);
            var models = new List<CreatePostInputModel>()
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
                    Title = "new2",
                    Category = "Action",
                    Content = "test2",
                    ImageUrl = "google2",
                },
                new CreatePostInputModel()
                {
                    Title = "new3",
                    Category = "Action",
                    Content = "test3",
                    ImageUrl = "google3",
                },
                new CreatePostInputModel()
                {
                    Title = "ne4",
                    Category = "Action",
                    Content = "test4",
                    ImageUrl = "google4",
                },
                new CreatePostInputModel()
                {
                    Title = "new5",
                    Category = "Action",
                    Content = "test5",
                    ImageUrl = "google5",
                },
                new CreatePostInputModel()
                {
                    Title = "new6",
                    Category = "Action",
                    Content = "test6",
                    ImageUrl = "google6",
                },
            };
            foreach (var model in models)
            {
                await service.CreateAsync(model, "1");
            }

            var actual = await service.GetAllAsync<PostViewModel>(5, 0);
            Assert.Equal(5, actual.Count());
        }

        [Fact]
        public async Task GetByCategoryShouldTakeTwo()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var context = new ApplicationDbContext(options.Options);
            await context.Users.AddAsync(new ApplicationUser() { Id = "1" });

            var repository = new EfDeletableEntityRepository<Post>(context);
            var service = new PostService(repository);
            var models = new List<CreatePostInputModel>()
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
                    Title = "new2",
                    Category = "Action",
                    Content = "test2",
                    ImageUrl = "google2",
                },
                new CreatePostInputModel()
                {
                    Title = "new3",
                    Category = "Rpg",
                    Content = "test3",
                    ImageUrl = "google3",
                },
            };
            foreach (var model in models)
            {
                await service.CreateAsync(model, "1");
            }

            var actual = await service.GetByCategoryAsync<PostViewModel>(CategoryOfPost.Action, 5, 0);
            Assert.Equal(2, actual.Count());
        }

        [Fact]
        public async Task GetByUserShouldTakeTwo()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var context = new ApplicationDbContext(options.Options);
            await context.Users.AddAsync(new ApplicationUser() { Id = "1" });

            var repository = new EfDeletableEntityRepository<Post>(context);
            var service = new PostService(repository);
            var models = new List<CreatePostInputModel>()
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
                    Title = "new2",
                    Category = "Action",
                    Content = "test2",
                    ImageUrl = "google2",
                },
            };
            foreach (var model in models)
            {
                await service.CreateAsync(model, "1");
            }

            var lastModel = new CreatePostInputModel()
            {
                Title = "new3",
                Category = "Rpg",
                Content = "test3",
                ImageUrl = "google3",
            };
            await service.CreateAsync(lastModel, "2");

            var actual = await service.GetByUserAsync<PostViewModel>("1", 5, 0);
            Assert.Equal(2, actual.Count());
        }

        [Fact]
        public async Task GetCountByUserShouldTake2()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var context = new ApplicationDbContext(options.Options);

            var repository = new EfDeletableEntityRepository<Post>(context);
            var service = new PostService(repository);
            var models = new List<CreatePostInputModel>()
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
                    Title = "new2",
                    Category = "Action",
                    Content = "test2",
                    ImageUrl = "google2",
                },
            };
            foreach (var model in models)
            {
                await service.CreateAsync(model, "1");
            }

            var newModel = new CreatePostInputModel()
            {
                Title = "new3",
                Category = "Action",
                Content = "test3",
                ImageUrl = "google3",
            };
            await service.CreateAsync(newModel, "2");

            var actual = await service.GetCountByUserAsync("1");
            Assert.Equal(2, actual);
        }

        [Fact]
        public async Task GetCountByCategoryShouldTake2()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var context = new ApplicationDbContext(options.Options);

            var repository = new EfDeletableEntityRepository<Post>(context);
            var service = new PostService(repository);
            var models = new List<CreatePostInputModel>()
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
                    Title = "new2",
                    Category = "Action",
                    Content = "test2",
                    ImageUrl = "google2",
                },
                new CreatePostInputModel()
                {
                    Title = "new3",
                    Category = "Rpg",
                    Content = "test3",
                    ImageUrl = "google3",
                },
            };
            foreach (var model in models)
            {
                await service.CreateAsync(model, "1");
            }

            var actual = await service.GetCountByCategoryAsync(CategoryOfPost.Action);
            Assert.Equal(2, actual);
        }

        [Fact]
        public async Task DetailsShouldShow()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var context = new ApplicationDbContext(options.Options);
            await context.Users.AddAsync(new ApplicationUser() { Id = "1" });

            var repository = new EfDeletableEntityRepository<Post>(context);
            var service = new PostService(repository);
            var model = new CreatePostInputModel()
            {
                Title = "new",
                Category = "Action",
                Content = "test",
                ImageUrl = "google",
            };
            var id = await service.CreateAsync(model, "1");

            var actual = await service.DetailsAsync<PostViewModel>(id);
            Assert.IsType<PostViewModel>(actual);
        }

        [Fact]
        public async Task GetRecentShouldTakeFive()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var context = new ApplicationDbContext(options.Options);
            await context.Users.AddAsync(new ApplicationUser() { Id = "1" });

            var repository = new EfDeletableEntityRepository<Post>(context);
            var service = new PostService(repository);
            var models = new List<CreatePostInputModel>()
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
                    Title = "new2",
                    Category = "Action",
                    Content = "test2",
                    ImageUrl = "google2",
                },
                new CreatePostInputModel()
                {
                    Title = "new3",
                    Category = "Rpg",
                    Content = "test3",
                    ImageUrl = "google3",
                },
                new CreatePostInputModel()
                {
                    Title = "new3",
                    Category = "Rpg",
                    Content = "test3",
                    ImageUrl = "google3",
                },
                new CreatePostInputModel()
                {
                    Title = "new3",
                    Category = "Rpg",
                    Content = "test3",
                    ImageUrl = "google3",
                },
                new CreatePostInputModel()
                {
                    Title = "new3",
                    Category = "Rpg",
                    Content = "test3",
                    ImageUrl = "google3",
                }
            };
            foreach (var model in models)
            {
                await service.CreateAsync(model, "1");
            }

            var actual = await service.GetRecentAsync<PostViewModel>();
            Assert.Equal(5, actual.Count());
        }

        [Fact]
        public async Task GetPostByIdShouldReturnModel()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var context = new ApplicationDbContext(options.Options);
            await context.Users.AddAsync(new ApplicationUser() { Id = "1" });

            var repository = new EfDeletableEntityRepository<Post>(context);
            var service = new PostService(repository);
            var model = new CreatePostInputModel()
            {
                Title = "new",
                Category = "Action",
                Content = "test",
                ImageUrl = "google",
            };

            var postId = await service.CreateAsync(model, "1");
            var actual = await service.GetPostByIdAsync<PostViewModel>(postId);
            Assert.IsType<PostViewModel>(actual);
        }

        [Fact]
        public async Task GetPostByIdShouldNotReturnModel()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var context = new ApplicationDbContext(options.Options);
            await context.Users.AddAsync(new ApplicationUser() { Id = "1" });

            var repository = new EfDeletableEntityRepository<Post>(context);
            var service = new PostService(repository);
            var model = new CreatePostInputModel()
            {
                Title = "new",
                Category = "Action",
                Content = "test",
                ImageUrl = "google",
            };

            await service.CreateAsync(model, "1");
            var actual = await service.GetPostByIdAsync<PostViewModel>("1234");
            Assert.Null(actual);
        }

        [Fact]
        public async Task DeletePostShouldDelete()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var context = new ApplicationDbContext(options.Options);
            await context.Users.AddAsync(new ApplicationUser() { Id = "1" });

            var repository = new EfDeletableEntityRepository<Post>(context);
            var service = new PostService(repository);
            var model = new CreatePostInputModel()
            {
                Title = "new",
                Category = "Action",
                Content = "test",
                ImageUrl = "google",
            };

            var postId = await service.CreateAsync(model, "1");
            await service.DeletePostAsync(postId);

            var actual = await service.GetAllCountAsync();
            Assert.Equal(0, actual);
        }

        [Fact]
        public async Task DoesPostBelongToUserShouldReturnTrue()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var context = new ApplicationDbContext(options.Options);
            await context.Users.AddAsync(new ApplicationUser() { Id = "1" });

            var repository = new EfDeletableEntityRepository<Post>(context);
            var service = new PostService(repository);
            var model = new CreatePostInputModel()
            {
                Title = "new",
                Category = "Action",
                Content = "test",
                ImageUrl = "google",
            };

            var postId = await service.CreateAsync(model, "1");

            var actual = await service.DoesPostBelongToUserAsync("1", postId);
            Assert.True(actual);
        }

        [Fact]
        public async Task DoesPostBelongToUserShouldReturnFalse()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var context = new ApplicationDbContext(options.Options);
            await context.Users.AddAsync(new ApplicationUser() { Id = "1" });
            await context.Users.AddAsync(new ApplicationUser() { Id = "2" });

            var repository = new EfDeletableEntityRepository<Post>(context);
            var service = new PostService(repository);
            var model = new CreatePostInputModel()
            {
                Title = "new",
                Category = "Action",
                Content = "test",
                ImageUrl = "google",
            };

            var postId = await service.CreateAsync(model, "2");

            var actual = await service.DoesPostBelongToUserAsync("1", postId);
            Assert.False(actual);
        }

        [Fact]
        public async Task EditPostShouldEditContent()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var context = new ApplicationDbContext(options.Options);
            await context.Users.AddAsync(new ApplicationUser() { Id = "2" });

            var repository = new EfDeletableEntityRepository<Post>(context);
            var service = new PostService(repository);
            var model = new CreatePostInputModel()
            {
                Title = "new",
                Category = "Action",
                Content = "test",
                ImageUrl = "google",
            };

            var postId = await service.CreateAsync(model, "2");
            var oldPost = await service.GetPostByIdAsync<EditPostViewModel>(postId);
            var contentOldValue = oldPost.Content;

            await service.EditPostAsync(oldPost.Id, oldPost.Title, "NewContentMaaaan");
            var newPost = await service.GetPostByIdAsync<EditPostViewModel>(postId);

            Assert.NotEqual(contentOldValue, newPost.Content);
        }

        [Fact]
        public async Task EditPostShouldEditTitle()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var context = new ApplicationDbContext(options.Options);
            await context.Users.AddAsync(new ApplicationUser() { Id = "2" });

            var repository = new EfDeletableEntityRepository<Post>(context);
            var service = new PostService(repository);
            var model = new CreatePostInputModel()
            {
                Title = "new",
                Category = "Action",
                Content = "test",
                ImageUrl = "google",
            };

            var postId = await service.CreateAsync(model, "2");
            var oldPost = await service.GetPostByIdAsync<EditPostViewModel>(postId);
            var titleOldValue = oldPost.Title;

            await service.EditPostAsync(oldPost.Id, "NewValue", oldPost.Content);
            var newPost = await service.GetPostByIdAsync<EditPostViewModel>(postId);

            Assert.NotEqual(titleOldValue, newPost.Title);
        }
    }
}
