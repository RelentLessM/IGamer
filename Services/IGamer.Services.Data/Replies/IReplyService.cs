namespace IGamer.Services.Data.Replies
{
    using System.Threading.Tasks;

    public interface IReplyService
    {
        Task<int> AddReplyToPostCommentAsync<T>(T model);

        Task<T> GetReplyByIdAsync<T>(int replyId);
    }
}
