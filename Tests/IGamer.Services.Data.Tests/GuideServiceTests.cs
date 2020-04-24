using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using IGamer.Data;
using IGamer.Data.Models;
using IGamer.Data.Models.Enums;
using IGamer.Data.Repositories;
using IGamer.Services.Data.Guides;
using IGamer.Services.Data.Posts;
using IGamer.Services.Mapping;
using IGamer.Web.ViewModels.Guides;
using IGamer.Web.ViewModels.Posts;
using IGamer.Web.ViewModels.ViewComponents;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace IGamer.Services.Data.Tests
{
    public class GuideServiceTests
    {
        public GuideServiceTests()
        {
            AutoMapperConfig
                .RegisterMappings(typeof(CreateGuideInputModel).GetTypeInfo().Assembly);
        }

        [Fact]
        public async Task AddOnGuideShouldAdd()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var context = new ApplicationDbContext(options.Options);

            var repository = new EfDeletableEntityRepository<Guide>(context);
            var service = new GuidesService(repository);
            var model = new CreateGuideInputModel()
            {
                Title = "test",
                Category = "Action",
                Description = "test",
                GameId = "1",
                ImageUrl = "test",
            };

            await service.CreateAsync(model, "1");
            var actual = await service.GetAllCountAsync();
            Assert.Equal(1, actual);
        }

        [Fact]
        public async Task GetAllShouldGetFive()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var context = new ApplicationDbContext(options.Options);
            await context.Users.AddAsync(new ApplicationUser() { Id = "1" });
            await context.Games.AddAsync(new Game() { Id = "1" });

            var repository = new EfDeletableEntityRepository<Guide>(context);
            var service = new GuidesService(repository);
            var models = new List<CreateGuideInputModel>()
            {
                new CreateGuideInputModel()
                {
                    Title = "new",
                    GameId = "1",
                    Category = "Action",
                    Description = "test",
                    ImageUrl = "google",
                },
                new CreateGuideInputModel()
                {
                    Title = "new2",
                    GameId = "1",
                    Category = "Action",
                    Description = "test2",
                    ImageUrl = "google2",
                },
                new CreateGuideInputModel()
                {
                    Title = "new3",
                    GameId = "1",
                    Category = "Action",
                    Description = "test3",
                    ImageUrl = "google3",
                },
                new CreateGuideInputModel()
                {
                    Title = "new4",
                    GameId = "1",
                    Category = "Action",
                    Description = "test4",
                    ImageUrl = "google4",
                },
                new CreateGuideInputModel()
                {
                    Title = "new5",
                    GameId = "1",
                    Category = "Action",
                    Description = "test5",
                    ImageUrl = "google5",
                },
                new CreateGuideInputModel()
                {
                    Title = "new6",
                    GameId = "1",
                    Category = "Action",
                    Description = "test6",
                    ImageUrl = "google6",
                },
            };

            foreach (var model in models)
            {
                await service.CreateAsync(model, "1");
            }

            var actual = await service.GetAllAsync<GuideViewModel>(5, 0);
            Assert.Equal(5, actual.Count());
        }

        [Fact]
        public async Task GetByIdShouldGetModel()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var context = new ApplicationDbContext(options.Options);
            await context.Users.AddAsync(new ApplicationUser() { Id = "1" });
            await context.Games.AddAsync(new Game() { Id = "1" });

            var repository = new EfDeletableEntityRepository<Guide>(context);
            var service = new GuidesService(repository);
            var model = new CreateGuideInputModel()
            {
                Title = "new",
                GameId = "1",
                Category = "Action",
                Description = "test",
                ImageUrl = "google",
            };
            var id = await service.CreateAsync(model, "1");

            var actual = await service.GetByIdAsync<GuideViewModel>(id);
            Assert.IsType<GuideViewModel>(actual);
        }

        [Fact]
        public async Task GetByIdShouldNotGetModel()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var context = new ApplicationDbContext(options.Options);
            await context.Users.AddAsync(new ApplicationUser() { Id = "1" });
            await context.Games.AddAsync(new Game() { Id = "1" });

            var repository = new EfDeletableEntityRepository<Guide>(context);
            var service = new GuidesService(repository);
            var model = new CreateGuideInputModel()
            {
                Title = "new",
                GameId = "1",
                Category = "Action",
                Description = "test",
                ImageUrl = "google",
            };
            var id = await service.CreateAsync(model, "1");

            var actual = await service.GetByIdAsync<GuideViewModel>("123");
            Assert.Null(actual);
        }

        [Fact]
        public async Task GetByCategoryShouldGetFive()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var context = new ApplicationDbContext(options.Options);
            await context.Users.AddAsync(new ApplicationUser() { Id = "1" });
            await context.Games.AddAsync(new Game() { Id = "1" });

            var repository = new EfDeletableEntityRepository<Guide>(context);
            var service = new GuidesService(repository);
            var models = new List<CreateGuideInputModel>()
            {
                new CreateGuideInputModel()
                {
                    Title = "new",
                    GameId = "1",
                    Category = "Action",
                    Description = "test",
                    ImageUrl = "google",
                },
                new CreateGuideInputModel()
                {
                    Title = "new2",
                    GameId = "1",
                    Category = "Action",
                    Description = "test2",
                    ImageUrl = "google2",
                },
                new CreateGuideInputModel()
                {
                    Title = "new3",
                    GameId = "1",
                    Category = "Action",
                    Description = "test3",
                    ImageUrl = "google3",
                },
                new CreateGuideInputModel()
                {
                    Title = "new4",
                    GameId = "1",
                    Category = "Action",
                    Description = "test4",
                    ImageUrl = "google4",
                },
                new CreateGuideInputModel()
                {
                    Title = "new5",
                    GameId = "1",
                    Category = "Action",
                    Description = "test5",
                    ImageUrl = "google5",
                },
                new CreateGuideInputModel()
                {
                    Title = "new6",
                    GameId = "1",
                    Category = "Action",
                    Description = "test6",
                    ImageUrl = "google6",
                },
            };

            foreach (var model in models)
            {
                await service.CreateAsync(model, "1");
            }

            var actual = await service.GetByCategoryAsync<GuideViewModel>(CategoryOfGuide.Action, 5, 0);
            Assert.Equal(5, actual.Count());
        }

        [Fact]
        public async Task GetByUserShouldGetFive()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var context = new ApplicationDbContext(options.Options);
            await context.Users.AddAsync(new ApplicationUser() { Id = "1" });
            await context.Games.AddAsync(new Game() { Id = "1" });

            var repository = new EfDeletableEntityRepository<Guide>(context);
            var service = new GuidesService(repository);
            var models = new List<CreateGuideInputModel>()
            {
                new CreateGuideInputModel()
                {
                    Title = "new",
                    GameId = "1",
                    Category = "Action",
                    Description = "test",
                    ImageUrl = "google",
                },
                new CreateGuideInputModel()
                {
                    Title = "new2",
                    GameId = "1",
                    Category = "Action",
                    Description = "test2",
                    ImageUrl = "google2",
                },
                new CreateGuideInputModel()
                {
                    Title = "new3",
                    GameId = "1",
                    Category = "Action",
                    Description = "test3",
                    ImageUrl = "google3",
                },
                new CreateGuideInputModel()
                {
                    Title = "new4",
                    GameId = "1",
                    Category = "Action",
                    Description = "test4",
                    ImageUrl = "google4",
                },
                new CreateGuideInputModel()
                {
                    Title = "new5",
                    GameId = "1",
                    Category = "Action",
                    Description = "test5",
                    ImageUrl = "google5",
                },
                new CreateGuideInputModel()
                {
                    Title = "new6",
                    GameId = "1",
                    Category = "Action",
                    Description = "test6",
                    ImageUrl = "google6",
                },
            };

            foreach (var model in models)
            {
                await service.CreateAsync(model, "1");
            }

            var actual = await service.GetByUserAsync<GuideViewModel>("1", 5, 0);
            Assert.Equal(5, actual.Count());
        }

        [Fact]
        public async Task GetRecentShouldGetFive()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var context = new ApplicationDbContext(options.Options);
            await context.Users.AddAsync(new ApplicationUser() { Id = "1" });
            await context.Games.AddAsync(new Game() { Id = "1" });

            var repository = new EfDeletableEntityRepository<Guide>(context);
            var service = new GuidesService(repository);
            var models = new List<CreateGuideInputModel>()
            {
                new CreateGuideInputModel()
                {
                    Title = "new",
                    GameId = "1",
                    Category = "Action",
                    Description = "test",
                    ImageUrl = "google",
                },
                new CreateGuideInputModel()
                {
                    Title = "new2",
                    GameId = "1",
                    Category = "Action",
                    Description = "test2",
                    ImageUrl = "google2",
                },
                new CreateGuideInputModel()
                {
                    Title = "new3",
                    GameId = "1",
                    Category = "Action",
                    Description = "test3",
                    ImageUrl = "google3",
                },
                new CreateGuideInputModel()
                {
                    Title = "new4",
                    GameId = "1",
                    Category = "Action",
                    Description = "test4",
                    ImageUrl = "google4",
                },
                new CreateGuideInputModel()
                {
                    Title = "new5",
                    GameId = "1",
                    Category = "Action",
                    Description = "test5",
                    ImageUrl = "google5",
                },
                new CreateGuideInputModel()
                {
                    Title = "new6",
                    GameId = "1",
                    Category = "Action",
                    Description = "test6",
                    ImageUrl = "google6",
                },
            };

            foreach (var model in models)
            {
                await service.CreateAsync(model, "1");
            }

            var actual = await service.GetRecentAsync<GuideViewModel>();
            Assert.Equal(5, actual.Count());
        }

        [Fact]
        public async Task GetCountByCategoryShouldGetThree()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var context = new ApplicationDbContext(options.Options);
            await context.Users.AddAsync(new ApplicationUser() { Id = "1" });
            await context.Games.AddAsync(new Game() { Id = "1" });

            var repository = new EfDeletableEntityRepository<Guide>(context);
            var service = new GuidesService(repository);
            var models = new List<CreateGuideInputModel>()
            {
                new CreateGuideInputModel()
                {
                    Title = "new",
                    GameId = "1",
                    Category = "Action",
                    Description = "test",
                    ImageUrl = "google",
                },
                new CreateGuideInputModel()
                {
                    Title = "new2",
                    GameId = "1",
                    Category = "Action",
                    Description = "test2",
                    ImageUrl = "google2",
                },
                new CreateGuideInputModel()
                {
                    Title = "new3",
                    GameId = "1",
                    Category = "Mmo",
                    Description = "test3",
                    ImageUrl = "google3",
                },
                new CreateGuideInputModel()
                {
                    Title = "new4",
                    GameId = "1",
                    Category = "Mmo",
                    Description = "test4",
                    ImageUrl = "google4",
                },
                new CreateGuideInputModel()
                {
                    Title = "new5",
                    GameId = "1",
                    Category = "Action",
                    Description = "test5",
                    ImageUrl = "google5",
                },
                new CreateGuideInputModel()
                {
                    Title = "new6",
                    GameId = "1",
                    Category = "Mmo",
                    Description = "test6",
                    ImageUrl = "google6",
                },
            };

            foreach (var model in models)
            {
                await service.CreateAsync(model, "1");
            }

            var actual = await service.GetCountByCategoryAsync(CategoryOfGuide.Action);
            Assert.Equal(3, actual);
        }

        [Fact]
        public async Task GetCountByUserShouldGetThree()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var context = new ApplicationDbContext(options.Options);
            await context.Users.AddAsync(new ApplicationUser() { Id = "1" });
            await context.Games.AddAsync(new Game() { Id = "1" });

            var repository = new EfDeletableEntityRepository<Guide>(context);
            var service = new GuidesService(repository);
            var models = new List<CreateGuideInputModel>()
            {
                new CreateGuideInputModel()
                {
                    Title = "new",
                    GameId = "1",
                    Category = "Action",
                    Description = "test",
                    ImageUrl = "google",
                },
                new CreateGuideInputModel()
                {
                    Title = "new2",
                    GameId = "1",
                    Category = "Action",
                    Description = "test2",
                    ImageUrl = "google2",
                },
                new CreateGuideInputModel()
                {
                    Title = "new3",
                    GameId = "1",
                    Category = "Mmo",
                    Description = "test3",
                    ImageUrl = "google3",
                },
            };

            foreach (var model in models)
            {
                await service.CreateAsync(model, "1");
            }

            var lastModel = new CreateGuideInputModel()
            {
                Title = "new4",
                GameId = "1",
                Category = "Mmo",
                Description = "test4",
                ImageUrl = "google4",
            };
            await service.CreateAsync(lastModel, "2");

            var actual = await service.GetCountByUserAsync("1");
            Assert.Equal(3, actual);
        }

        [Fact]
        public async Task DoesGuideBelongToUserShouldReturnTrue()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var context = new ApplicationDbContext(options.Options);
            await context.Users.AddAsync(new ApplicationUser() { Id = "1" });
            await context.Games.AddAsync(new Game() { Id = "1" });

            var repository = new EfDeletableEntityRepository<Guide>(context);
            var service = new GuidesService(repository);
            var model = new CreateGuideInputModel()
            {
                Title = "new",
                GameId = "1",
                Category = "Action",
                Description = "test",
                ImageUrl = "google",
            };
            var modelId = await service.CreateAsync(model, "1");

            var actual = await service.DoesGuideBelongToUserAsync("1", modelId);
            Assert.True(actual);
        }

        [Fact]
        public async Task DoesGuideBelongToUserShouldReturnFalse()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var context = new ApplicationDbContext(options.Options);
            await context.Users.AddAsync(new ApplicationUser() { Id = "1" });
            await context.Users.AddAsync(new ApplicationUser() { Id = "2" });
            await context.Games.AddAsync(new Game() { Id = "1" });

            var repository = new EfDeletableEntityRepository<Guide>(context);
            var service = new GuidesService(repository);
            var model = new CreateGuideInputModel()
            {
                Title = "new",
                GameId = "1",
                Category = "Action",
                Description = "test",
                ImageUrl = "google",
            };
            var modelId = await service.CreateAsync(model, "1");

            var actual = await service.DoesGuideBelongToUserAsync("2", modelId);
            Assert.False(actual);
        }

        [Fact]
        public async Task DeleteGuideShouldDelete()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var context = new ApplicationDbContext(options.Options);
            await context.Users.AddAsync(new ApplicationUser() { Id = "1" });
            await context.Games.AddAsync(new Game() { Id = "1" });

            var repository = new EfDeletableEntityRepository<Guide>(context);
            var service = new GuidesService(repository);
            var model = new CreateGuideInputModel()
            {
                Title = "new",
                GameId = "1",
                Category = "Action",
                Description = "test",
                ImageUrl = "google",
            };
            var modelId = await service.CreateAsync(model, "1");

            await service.DeleteGuideAsync(modelId);

            var actual = await service.GetAllCountAsync();
            Assert.Equal(0, actual);
        }

        [Fact]
        public async Task GetAllForAdminShouldGetFive()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var context = new ApplicationDbContext(options.Options);
            await context.Users.AddAsync(new ApplicationUser() { Id = "1" });
            await context.Games.AddAsync(new Game() { Id = "1" });

            var repository = new EfDeletableEntityRepository<Guide>(context);
            var service = new GuidesService(repository);
            var models = new List<CreateGuideInputModel>()
            {
                new CreateGuideInputModel()
                {
                    Title = "new",
                    GameId = "1",
                    Category = "Action",
                    Description = "test",
                    ImageUrl = "google",
                },
                new CreateGuideInputModel()
                {
                    Title = "new2",
                    GameId = "1",
                    Category = "Action",
                    Description = "test2",
                    ImageUrl = "google2",
                },
                new CreateGuideInputModel()
                {
                    Title = "new3",
                    GameId = "1",
                    Category = "Action",
                    Description = "test3",
                    ImageUrl = "google3",
                },
                new CreateGuideInputModel()
                {
                    Title = "new4",
                    GameId = "1",
                    Category = "Action",
                    Description = "test4",
                    ImageUrl = "google4",
                },
                new CreateGuideInputModel()
                {
                    Title = "new5",
                    GameId = "1",
                    Category = "Action",
                    Description = "test5",
                    ImageUrl = "google5",
                },
                new CreateGuideInputModel()
                {
                    Title = "new6",
                    GameId = "1",
                    Category = "Action",
                    Description = "test6",
                    ImageUrl = "google6",
                },
            };

            foreach (var model in models)
            {
                await service.CreateAsync(model, "1");
            }

            var actual = await service.GetAllForAdminAsync<GuideViewModel>(5, 0);
            Assert.Equal(5, actual.Count());
        }

        [Fact]
        public async Task GetLatestShouldReturnModel()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var context = new ApplicationDbContext(options.Options);
            await context.Users.AddAsync(new ApplicationUser() { Id = "1" });
            await context.Games.AddAsync(new Game() { Id = "1" });

            var repository = new EfDeletableEntityRepository<Guide>(context);
            var service = new GuidesService(repository);
            var model = new CreateGuideInputModel()
            {
                Title = "new",
                GameId = "1",
                Category = "Action",
                Description = "test",
                ImageUrl = "google",
            };
            await service.CreateAsync(model, "1");

            var actual = await service.TakeLatestGuideAsync<LatestGuideViewModel>();
            Assert.IsType<LatestGuideViewModel>(actual);
        }

        [Fact]
        public async Task EditGuideShouldEditTitle()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var context = new ApplicationDbContext(options.Options);
            await context.Users.AddAsync(new ApplicationUser() { Id = "1" });
            await context.Games.AddAsync(new Game() { Id = "1" });

            var repository = new EfDeletableEntityRepository<Guide>(context);
            var service = new GuidesService(repository);
            var model = new CreateGuideInputModel()
            {
                Title = "new",
                GameId = "1",
                Category = "Action",
                Description = "test",
                ImageUrl = "google",
            };

            var guideId = await service.CreateAsync(model, "1");
            var oldGuide = await service.GetByIdAsync<EditGuideViewModel>(guideId);
            var titleOldValue = oldGuide.Title;

            await service.EditGuideAsync(oldGuide.Id, "NewValue", oldGuide.Content);
            var newGuide = await service.GetByIdAsync<EditGuideViewModel>(guideId);

            Assert.NotEqual(titleOldValue, newGuide.Title);
        }

        [Fact]
        public async Task EditGuideShouldEditContent()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var context = new ApplicationDbContext(options.Options);
            await context.Users.AddAsync(new ApplicationUser() { Id = "1" });
            await context.Games.AddAsync(new Game() { Id = "1" });

            var repository = new EfDeletableEntityRepository<Guide>(context);
            var service = new GuidesService(repository);
            var model = new CreateGuideInputModel()
            {
                Title = "new",
                GameId = "1",
                Category = "Action",
                Description = "test",
                ImageUrl = "google",
            };

            var guideId = await service.CreateAsync(model, "1");
            var oldGuide = await service.GetByIdAsync<EditGuideViewModel>(guideId);
            var contentOldValue = oldGuide.Content;

            await service.EditGuideAsync(oldGuide.Id, oldGuide.Title, "newwwwwwwwwwwwwwwwwwwwwwwwwwwww");
            var newGuide = await service.GetByIdAsync<EditGuideViewModel>(guideId);

            Assert.NotEqual(contentOldValue, newGuide.Title);
        }
    }
}
