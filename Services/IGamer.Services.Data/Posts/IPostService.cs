namespace IGamer.Services.Data.Posts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using IGamer.Data.Models.Enums;

    public interface IPostService
    {
        Task<IEnumerable<T>> GetAllAsync<T>();

        Task<IEnumerable<T>> GetByCategoryAsync<T>(CategoryOfPost categoryName);

        Task<IEnumerable<T>> GetByUserAsync<T>(string userId);

        Task<string> CreateAsync<T>(T model, string userId);

        Task<T> DetailsAsync<T>(string id);
    }
}
