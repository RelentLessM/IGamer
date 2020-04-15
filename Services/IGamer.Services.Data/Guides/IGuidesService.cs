﻿
namespace IGamer.Services.Data.Guides
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using IGamer.Data.Models.Enums;

    public interface IGuidesService
    {
        Task<IEnumerable<T>> GetAllAsync<T>();

        Task<int> GetAllCountAsync();

        Task<string> CreateAsync<T>(T model, string userId);

        Task<T> GetByIdAsync<T>(string id);

        Task<IEnumerable<T>> GetByCategoryAsync<T>(CategoryOfGuide categoryName, int take, int skip = 0);

        Task<int> GetCountByCategoryAsync(CategoryOfGuide categoryName);

        Task<IEnumerable<T>> GetRecentAsync<T>();
    }
}
