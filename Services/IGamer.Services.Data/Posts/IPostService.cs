namespace IGamer.Services.Data.Posts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IPostService
    {
        //IQueryable<T> GetAll<T>();
        IEnumerable<T> GetAll<T>();

        // Task<string> Create(CreatePostServiceModel model);
        Task<string> Create<T>(T model, string userId);

        Task<T> Details<T>(string id);
    }
}
