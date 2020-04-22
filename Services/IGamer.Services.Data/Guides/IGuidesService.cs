
namespace IGamer.Services.Data.Guides
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using IGamer.Data.Models.Enums;

    public interface IGuidesService
    {
        Task<IEnumerable<T>> GetAllAsync<T>(int take, int skip = 0);

        Task<int> GetAllCountAsync();

        Task<string> CreateAsync<T>(T model, string userId);

        Task<T> GetByIdAsync<T>(string id);

        Task<IEnumerable<T>> GetByCategoryAsync<T>(CategoryOfGuide categoryName, int take, int skip = 0);

        Task<int> GetCountByCategoryAsync(CategoryOfGuide categoryName);

        Task<IEnumerable<T>> GetRecentAsync<T>();

        Task<IEnumerable<T>> GetByUserAsync<T>(string userId, int take, int skip = 0);

        Task<int> GetCountByUserAsync(string userId);

        Task<bool> DoesGuideBelongToUserAsync(string userId, string id);

        Task<T> GetGuideByIdAsync<T>(string id);

        Task DeleteGuideAsync(string id);

        Task<IEnumerable<T>> GetAllForAdminAsync<T>(int take, int skip = 0);
    }
}
