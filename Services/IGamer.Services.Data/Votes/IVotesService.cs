using System.Threading.Tasks;

namespace IGamer.Services.Data.Votes
{
    public interface IVotesService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="postId"></param>
        /// <param name="userId"></param>
        /// <param name="isUpVote">If true - like, else - dislike</param>
        /// <returns></returns>
        Task VoteOnPostAsync(string postId, string userId, bool isUpVote);

        Task<int> GetVotesAsync(string postId);
    }
}
