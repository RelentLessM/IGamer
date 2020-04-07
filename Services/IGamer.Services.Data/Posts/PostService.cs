namespace IGamer.Services.Data.Posts
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using IGamer.Data.Common.Repositories;
    using IGamer.Data.Models;
    using IGamer.Data.Models.Enums;
    using IGamer.Services.Mapping;
    using Microsoft.EntityFrameworkCore;

    public class PostService : IPostService
    {
        private readonly IDeletableEntityRepository<Post> repository;

        public PostService(IDeletableEntityRepository<Post> repository)
        {
            this.repository = repository;
        }

        public async Task<IEnumerable<T>> GetAllAsync<T>()
            => await this.repository.All().OrderByDescending(x => x.CreatedOn).To<T>().ToListAsync();

        public async Task<IEnumerable<T>> GetByCategoryAsync<T>(CategoryOfPost categoryName)
            => await this.repository.All().Where(x => x.Category == categoryName).OrderByDescending(x => x.CreatedOn).To<T>().ToListAsync();

        public async Task<IEnumerable<T>> GetByUserAsync<T>(string userId)
        => await this.repository.All().Where(x => x.UserId == userId).OrderByDescending(x => x.CreatedOn).To<T>().ToListAsync();

        public async Task<string> CreateAsync<T>(T model, string userId)
        {
            var post = AutoMapperConfig.MapperInstance.Map<Post>(model);
            post.UserId = userId;
            await this.repository.AddAsync(post);
            await this.repository.SaveChangesAsync();

            var postId = post.Id;

            return postId;
        }

        public async Task<T> DetailsAsync<T>(string id)
        => await this.repository.All().Where(x => x.Id == id).To<T>().FirstOrDefaultAsync();
    }
}
