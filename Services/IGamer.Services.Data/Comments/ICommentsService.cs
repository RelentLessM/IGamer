namespace IGamer.Services.Data.Comments
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICommentsService
    {
        Task<IEnumerable<T>> GetAllAsync<T>(string id);

        Task<int> AddCommentToPostAsync<T>(T model);

        Task<T> GetCommentByIdAsync<T>(int commentId);
    }
}
