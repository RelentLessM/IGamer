namespace IGamer.Services.Data.Tests
{
    using System;
    using System.Reflection;
    using System.Threading.Tasks;

    using IGamer.Data;
    using IGamer.Data.Models;
    using IGamer.Data.Repositories;
    using IGamer.Services.Data.Replies;
    using IGamer.Services.Mapping;
    using IGamer.Web.ViewModels.Replies;
    using IGamer.Web.ViewModels.Reports;
    using Microsoft.EntityFrameworkCore;
    using Xunit;

    public class RepliesServiceTests
    {
        public RepliesServiceTests()
        {
            AutoMapperConfig
                .RegisterMappings(typeof(AddReplyInputModel).GetTypeInfo().Assembly);
        }

        [Fact]
        public async Task AddOnReplyShouldAdd()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var context = new ApplicationDbContext(options.Options);

            var repository = new EfDeletableEntityRepository<ReplyOnPostComment>(context);
            var service = new ReplyService(repository);
            var model = new AddReplyInputModel()
            {
                UserId = "1",
                CommentId = 1,
                Description = "test reply",
                PostId = "1",
            };

            var replyId = await service.AddReplyToPostCommentAsync(model);

            Assert.Equal(1, replyId);
        }

        // TODO: Find why service is not taking any data when there is.
        [Fact]
        public async Task CountOfRepliesShouldBeOne()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var context = new ApplicationDbContext(options.Options);
            await context.Users.AddAsync(new ApplicationUser() { Id = "1" });
            var repository = new EfDeletableEntityRepository<ReplyOnPostComment>(context);
            var service = new ReplyService(repository);
            var model = new AddReplyInputModel()
            {
                UserId = "1",
                CommentId = 1,
                Description = "test reply",
                PostId = "1",
            };

            var replyId = await service.AddReplyToPostCommentAsync(model);
            var actual = await service.GetReplyByIdAsync<ReplyResponseModel>(replyId);

            Assert.IsType<ReplyResponseModel>(actual);
        }
    }
}
