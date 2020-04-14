
namespace IGamer.Services.Data.Guides
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IGuidesService
    {
        Task<IEnumerable<T>> GetAllAsync<T>();

        Task<string> CreateAsync<T>(T model, string userId);
    }
}
