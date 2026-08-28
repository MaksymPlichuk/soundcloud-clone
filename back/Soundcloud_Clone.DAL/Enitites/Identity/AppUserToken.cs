using Microsoft.AspNetCore.Identity;

namespace Soundcloud_Clone.DAL.Enitites.Identity
{
    public class AppUserToken : IdentityUserToken<int>
    {
        public virtual UserEntity User { get; set; }
    }
}
