// ReSharper disable VirtualMemberCallInConstructor
namespace IGamer.Data.Models
{
    using System;
    using System.Collections.Generic;

    using IGamer.Data.Common.Models;

    using Microsoft.AspNetCore.Identity;

    public class ApplicationUser : IdentityUser, IAuditInfo, IDeletableEntity
    {
        public ApplicationUser()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Roles = new HashSet<IdentityUserRole<string>>();
            this.Claims = new HashSet<IdentityUserClaim<string>>();
            this.Logins = new HashSet<IdentityUserLogin<string>>();
            this.Posts = new HashSet<Post>();
            this.Guides = new HashSet<Guide>();
            this.CommentsOnPosts = new HashSet<CommentOnPost>();
            this.Reports = new HashSet<Report>();
            this.Suggestions = new HashSet<SuggestionGame>();
            this.VotesForGuides = new HashSet<VoteOnGuide>();
            this.VotesForPosts = new HashSet<VoteOnPost>();
            this.VotesOnPostComments = new HashSet<VoteOnPostComment>();
        }

        // Audit info
        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        // Deletable entity
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public virtual ICollection<IdentityUserRole<string>> Roles { get; set; }

        public virtual ICollection<IdentityUserClaim<string>> Claims { get; set; }

        public virtual ICollection<IdentityUserLogin<string>> Logins { get; set; }

        public virtual ICollection<Post> Posts { get; set; }

        public virtual ICollection<CommentOnPost> CommentsOnPosts { get; set; }

        public virtual ICollection<Report> Reports { get; set; }

        public virtual ICollection<Guide> Guides { get; set; }

        public virtual ICollection<SuggestionGame> Suggestions { get; set; }

        public virtual ICollection<VoteOnGuide> VotesForGuides { get; set; }

        public virtual ICollection<VoteOnPost> VotesForPosts { get; set; }

        public virtual ICollection<VoteOnPostComment> VotesOnPostComments { get; set; }

        public string ImageUrl { get; set; }
    }
}
