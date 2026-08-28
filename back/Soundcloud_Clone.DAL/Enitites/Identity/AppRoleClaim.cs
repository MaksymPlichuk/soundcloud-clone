using Microsoft.AspNetCore.Identity;

namespace Soundcloud_Clone.DAL.Enitites.Identity
{
    public class AppRoleClaim : IdentityRoleClaim<int>
    {
        public virtual AppRole Role { get; set; }
    }

}
