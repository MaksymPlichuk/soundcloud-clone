

using Microsoft.AspNetCore.Identity;

namespace Soundcloud_Clone.DAL.Enitites.Identity
{
    public class AppUserClaim : IdentityUserClaim<int>
    {
        public virtual UserEntity User { get; set; }
    }
}
