namespace IGamer.Services.Data.Tests
{
    using System;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;

    using IGamer.Data;
    using IGamer.Data.Models;
    using IGamer.Data.Repositories;
    using IGamer.Services.Data.Comments;
    using IGamer.Services.Mapping;
    using IGamer.Web.ViewModels.Comments;
    using IGamer.Web.ViewModels.ViewComponents;
    using Microsoft.EntityFrameworkCore;
    using Xunit;

    public class CommentsServiceTests
    {
        public CommentsServiceTests()
        {
            AutoMapperConfig
                .RegisterMappings(typeof(CommentViewModel).GetTypeInfo().Assembly);
        }

        [Fact]
        public async Task AddOnCommentShouldAdd()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var context = new ApplicationDbContext(options.Options);
            await context.Users.AddAsync(new ApplicationUser() { Id = "1" });

            var repository = new EfDeletableEntityRepository<CommentOnPost>(context);
            var service = new CommentService(repository);
            var model = new AddCommentInputModel()
            {
                Description = "test",
                PostId = "1",
                UserId = "1",
            };

            await service.AddCommentToPostAsync(model);
            var actual = await service.GetAllAsync<CommentViewModel>("1");
            Assert.Single(actual);
        }

        [Fact]
        public async Task GetCommentByIdShouldGetOne()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var context = new ApplicationDbContext(options.Options);
            await context.Users.AddAsync(new ApplicationUser() { Id = "1" });

            var repository = new EfDeletableEntityRepository<CommentOnPost>(context);
            var service = new CommentService(repository);
            var model = new AddCommentInputModel()
            {
                Description = "test",
                PostId = "1",
                UserId = "1",
            };

            var id = await service.AddCommentToPostAsync(model);
            var actual = await service.GetCommentByIdAsync<CommentViewModel>(id);
            Assert.IsType<CommentViewModel>(actual);
        }

        [Fact]
        public async Task GetCommentByIdShouldNotGetOne()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var context = new ApplicationDbContext(options.Options);
            await context.Users.AddAsync(new ApplicationUser() { Id = "1" });

            var repository = new EfDeletableEntityRepository<CommentOnPost>(context);
            var service = new CommentService(repository);
            var model = new AddCommentInputModel()
            {
                Description = "test",
                PostId = "1",
                UserId = "1",
            };

            var id = await service.AddCommentToPostAsync(model);
            var actual = await service.GetCommentByIdAsync<CommentViewModel>(7);
            Assert.Null(actual);
        }

        [Fact]
        public async Task GetAllShouldGetTwo()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var context = new ApplicationDbContext(options.Options);
            await context.Users.AddAsync(new ApplicationUser() { Id = "1" });

            var repository = new EfDeletableEntityRepository<CommentOnPost>(context);
            var service = new CommentService(repository);
            var model = new AddCommentInputModel()
            {
                Description = "test",
                PostId = "1",
                UserId = "1",
            };
            var secondModel = new AddCommentInputModel()
            {
                Description = "test2",
                PostId = "1",
                UserId = "1",
            };

            await service.AddCommentToPostAsync(model);
            await service.AddCommentToPostAsync(secondModel);
            var actual = await service.GetAllAsync<CommentViewModel>("1");
            Assert.Equal(2, actual.Count());
        }
    }
}
