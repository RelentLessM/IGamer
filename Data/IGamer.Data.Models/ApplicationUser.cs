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
            this.Blogs = new HashSet<Blog>();
            this.Guides = new HashSet<Guide>();
            this.CommentsOnBlogs = new HashSet<CommentOnBlog>();
            this.CommentsOnGuide = new HashSet<CommentOnGuide>();
            this.Suggestions = new HashSet<SuggestionGame>();
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

        public virtual ICollection<Blog> Blogs { get; set; }

        public virtual ICollection<CommentOnBlog> CommentsOnBlogs { get; set; }

        public virtual ICollection<CommentOnGuide> CommentsOnGuide { get; set; }

        public virtual ICollection<Guide> Guides { get; set; }

        public virtual ICollection<SuggestionGame> Suggestions { get; set; }
    }
}
