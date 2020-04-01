namespace IGamer.Services.Data.Posts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using IGamer.Data.Models.Enums;

    public interface IPostService
    {
        IEnumerable<T> GetAll<T>();

        IEnumerable<T> GetByCategory<T>(CategoryOfPost categoryName);

        Task<string> CreateAsync<T>(T model, string userId);

        Task<T> DetailsAsync<T>(string id);
    }
}
