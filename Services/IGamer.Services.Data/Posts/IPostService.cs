namespace IGamer.Services.Data.Posts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using IGamer.Data.Models.Enums;

    public interface IPostService
    {
        Task<IEnumerable<T>> GetAllAsync<T>(int take, int skip = 0);

        Task<int> GetAllCountAsync();

        Task<IEnumerable<T>> GetByCategoryAsync<T>(CategoryOfPost categoryName, int take, int skip = 0);

        Task<int> GetCountByCategoryAsync(CategoryOfPost categoryName);

        Task<IEnumerable<T>> GetByUserAsync<T>(string userId, int take, int skip = 0);

        Task<int> GetCountByUserAsync(string userId);

        Task<string> CreateAsync<T>(T model, string userId);

        Task<T> DetailsAsync<T>(string id);
    }
}
