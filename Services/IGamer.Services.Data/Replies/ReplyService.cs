using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IGamer.Data.Common.Repositories;
using IGamer.Data.Models;
using IGamer.Services.Mapping;
using Microsoft.EntityFrameworkCore;

namespace IGamer.Services.Data.Replies
{
    public class ReplyService : IReplyService
    {
        private readonly IDeletableEntityRepository<ReplyOnPostComment> repository;

        public ReplyService(IDeletableEntityRepository<ReplyOnPostComment> repository)
        {
            this.repository = repository;
        }

        public IEnumerable<T> GetAll<T>(int id)
            => this.repository.All().Where(x => x.CommentId == id).To<T>().ToList();

        public async Task<int> AddReplyToPostCommentAsync<T>(T model)
        {
            var reply = AutoMapperConfig.MapperInstance.Map<ReplyOnPostComment>(model);

            await this.repository.AddAsync(reply);
            await this.repository.SaveChangesAsync();

            return reply.Id;
        }

        public async Task<T> GetReplyByIdAsync<T>(int replyId)
        {
            var reply = await this.repository.All().Where(x => x.Id == replyId).To<T>().FirstOrDefaultAsync();

            return reply;
        }
    }
}
