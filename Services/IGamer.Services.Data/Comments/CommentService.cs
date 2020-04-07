namespace IGamer.Services.Data.Comments
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using IGamer.Data.Common.Repositories;
    using IGamer.Data.Models;
    using IGamer.Services.Mapping;
    using Microsoft.EntityFrameworkCore;

    public class CommentService : ICommentsService
    {
        private readonly IDeletableEntityRepository<CommentOnPost> repository;

        public CommentService(IDeletableEntityRepository<CommentOnPost> repository)
        {
            this.repository = repository;
        }

        public async Task<IEnumerable<T>> GetAllAsync<T>(string id)
            => await this.repository.All().Where(x => x.PostId == id).OrderBy(x => x.CreatedOn).To<T>().ToListAsync();

        public async Task<int> AddCommentToPostAsync<T>(T model)
        {
            var comment = AutoMapperConfig.MapperInstance.Map<CommentOnPost>(model);

            await this.repository.AddAsync(comment);
            await this.repository.SaveChangesAsync();

            return comment.Id;
        }

        public async Task<T> GetCommentByIdAsync<T>(int commentId)
        {
            var comment = await this.repository.All().Where(x => x.Id == commentId).To<T>().FirstOrDefaultAsync();

            return comment;
        }
    }
}
