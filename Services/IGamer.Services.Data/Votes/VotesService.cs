namespace IGamer.Services.Data.Votes
{
    using System.Linq;
    using System.Threading.Tasks;

    using IGamer.Data.Common.Repositories;
    using IGamer.Data.Models;
    using IGamer.Data.Models.Enums;
    using Microsoft.EntityFrameworkCore;

    public class VotesService : IVotesService
    {
        private readonly IRepository<VoteOnPost> postRepository;
        private readonly IRepository<VoteOnGuide> guideRepository;

        public VotesService(IRepository<VoteOnPost> postRepository, IRepository<VoteOnGuide> guideRepository)
        {
            this.postRepository = postRepository;
            this.guideRepository = guideRepository;
        }

        public async Task VoteOnPostAsync(string postId, string userId, bool isUpVote)
        {
            var vote = await this.postRepository.All()
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

                await this.postRepository.AddAsync(vote);
            }

            await this.postRepository.SaveChangesAsync();
        }

        public async Task VoteOnGuideAsync(string guideId, string userId, bool isUpVote)
        {
            var vote = await this.guideRepository.All()
                .FirstOrDefaultAsync(x => x.GuideId == guideId && x.UserId == userId);

            if (vote != null)
            {
                vote.VoteType = isUpVote ? VoteType.Like : VoteType.Dislike;
            }
            else
            {
                vote = new VoteOnGuide()
                {
                    GuideId = guideId,
                    UserId = userId,
                    VoteType = isUpVote ? VoteType.Like : VoteType.Dislike,
                };

                await this.guideRepository.AddAsync(vote);
            }

            await this.guideRepository.SaveChangesAsync();
        }

        public async Task<int> GetVotesOnPostAsync(string postId)
            => await this.postRepository.All()
                .Where(x => x.PostId == postId)
                .SumAsync(x => (int)x.VoteType);

        public async Task<int> GetVotesOnGuideAsync(string guideId)
            => await this.guideRepository.All()
                .Where(x => x.GuideId == guideId)
                .SumAsync(x => (int)x.VoteType);
    }
}
