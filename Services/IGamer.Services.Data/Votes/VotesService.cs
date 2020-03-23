using System.Linq;

namespace IGamer.Services.Data.Votes
{
    using System.Threading.Tasks;

    using IGamer.Data.Common.Repositories;
    using IGamer.Data.Models;
    using IGamer.Data.Models.Enums;
    using Microsoft.EntityFrameworkCore;

    public class VotesService : IVotesService
    {
        private readonly IRepository<VoteOnPost> repository;

        public VotesService(IRepository<VoteOnPost> repository)
        {
            this.repository = repository;
        }

        public async Task VoteOnPostAsync(string postId, string userId, bool isUpVote)
        {
            var vote = await this.repository.All()
                .FirstOrDefaultAsync(x => x.PostId == postId && x.UserId == userId);

            if (vote != null)
            {
                vote.VoteType = isUpVote ? VoteType.Like : VoteType.Dislike;
            }
            else
            {
                vote = new VoteOnPost()
                {
                    PostId = postId,
                    UserId = userId,
                    VoteType = isUpVote ? VoteType.Like : VoteType.Dislike,
                };

                await this.repository.AddAsync(vote);
            }

            await this.repository.SaveChangesAsync();
        }

        public async Task<int> GetVotesAsync(string postId)
        {
            return await this.repository.All().Where(x => x.PostId == postId).SumAsync(x => (int)x.VoteType);
        }
    }
}
