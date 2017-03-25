using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace LazyVocabulary.DAL.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public virtual UserProfile UserProfile { get; set; }

        public virtual ICollection<Dictionary> Dictionaries { get; set; }

        public virtual ICollection<Subscription> TargetSubscriptions { get; set; }

        public virtual ICollection<Subscription> SubscriberSubscriptions { get; set; }

        public ApplicationUser()
        {
            Dictionaries = new List<Dictionary>();
            TargetSubscriptions = new List<Subscription>();
            SubscriberSubscriptions = new List<Subscription>();
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            return userIdentity;
        }
    }
}
