using System.Collections.Generic;
using System.Linq;

namespace IGamer.Services.Data.Posts
{
    using System.Threading.Tasks;

    using IGamer.Services.Data.ServiceModels;

    public interface IPostService
    {
        IQueryable<T> GetAll<T>();

        // Task<string> Create(CreatePostServiceModel model);
        Task<string> Create<T>(T model, string userId);
    }
}
