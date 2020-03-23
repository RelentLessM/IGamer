namespace IGamer.Services.Data.Comments
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICommentsService
    {
        IEnumerable<T> GetAll<T>(string id);

        Task<int> AddCommentAsync<T>(T model);

        Task<T> GetCommentByIdAsync<T>(int commentId);
    }
}
