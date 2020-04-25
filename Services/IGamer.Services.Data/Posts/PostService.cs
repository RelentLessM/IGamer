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

        public async Task<IEnumerable<T>> GetAllAsync<T>(int take, int skip = 0)
            => await this.repository.All()
                .OrderByDescending(x => x.CreatedOn)
                .Skip(skip)
                .Take(take)
                .To<T>()
                .ToListAsync();

        public async Task<int> GetAllCountAsync()
            => await this.repository.All()
                .CountAsync();

        public async Task<IEnumerable<T>> GetByCategoryAsync<T>(CategoryOfPost categoryName, int take, int skip = 0)
            => await this.repository.All()
                .Where(x => x.Category == categoryName)
                .OrderByDescending(x => x.CreatedOn)
                .Skip(skip)
                .Take(take)
                .To<T>()
                .ToListAsync();

        public async Task<int> GetCountByCategoryAsync(CategoryOfPost categoryName)
            => await this.repository.All()
                .Where(x => x.Category == categoryName)
                .CountAsync();

        public async Task<int> GetCountBySearchAsync(string search)
            => await this.repository.All()
                .Where(x => x.Description.Contains(search) || x.Title.Contains(search))
                .CountAsync();

        public async Task<IEnumerable<T>> GetByUserAsync<T>(string userId, int take, int skip = 0)
            => await this.repository.All()
                .Where(x => x.UserId == userId)
                .OrderByDescending(x => x.CreatedOn)
                .Skip(skip)
                .Take(take)
                .To<T>()
                .ToListAsync();

        public async Task<int> GetCountByUserAsync(string userId)
       => await this.repository.All()
           .Where(x => x.UserId == userId)
           .CountAsync();

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
            => await this.repository.All()
                .Where(x => x.Id == id).To<T>()
                .FirstOrDefaultAsync();

        public async Task<IEnumerable<T>> GetRecentAsync<T>()
            => await this.repository.All()
                .OrderByDescending(x => x.CreatedOn)
                .Take(5)
                .To<T>()
                .ToListAsync();

        public async Task<T> GetPostByIdAsync<T>(string id)
            => await this.repository.All().Where(x => x.Id == id).To<T>().FirstOrDefaultAsync();

        public async Task DeletePostAsync(string id)
        {
            var post = await this.repository.All().Where(x => x.Id == id).FirstOrDefaultAsync();

            this.repository.Delete(post);
            await this.repository.SaveChangesAsync();
        }

        public async Task<bool> DoesPostBelongToUserAsync(string userId, string postId)
            => await this.repository.All().AnyAsync(x => x.UserId == userId && x.Id == postId);

        public async Task EditPostAsync(string id, string title, string content)
        {
            var post = await this.repository.All().Where(x => x.Id == id).FirstOrDefaultAsync();
            post.Description = content;
            post.Title = title;
            this.repository.Update(post);
            await this.repository.SaveChangesAsync();
        }
    }
}
