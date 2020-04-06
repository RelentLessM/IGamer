using System.Collections.Generic;
using IGamer.Data.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace IGamer.Services.Data.Posts
{
    using System.Linq;
    using System.Threading.Tasks;

    using IGamer.Data.Common.Repositories;
    using IGamer.Data.Models;
    using IGamer.Services.Data.ServiceModels;
    using IGamer.Services.Mapping;

    public class PostService : IPostService
    {
        private readonly IDeletableEntityRepository<Post> repository;

        public PostService(IDeletableEntityRepository<Post> repository)
        {
            this.repository = repository;
        }

        public IEnumerable<T> GetAll<T>()
            => this.repository.All().OrderByDescending(x => x.CreatedOn).To<T>().ToList();

        public IEnumerable<T> GetByCategory<T>(CategoryOfPost categoryName)
            => this.repository.All().Where(x => x.Category == categoryName).OrderByDescending(x => x.CreatedOn).To<T>().ToList();

        public IEnumerable<T> GetByUser<T>(string userId)
        => this.repository.All().Where(x => x.UserId == userId).OrderByDescending(x => x.CreatedOn).To<T>().ToList();

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
        {
            var post = await this.repository.All().Where(x => x.Id == id).To<T>().FirstOrDefaultAsync();

            return post;
        }
    }
}
