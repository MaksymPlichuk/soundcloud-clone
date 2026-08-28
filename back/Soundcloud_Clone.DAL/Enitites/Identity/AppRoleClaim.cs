using Microsoft.AspNetCore.Identity;

namespace Soundcloud_Clone.DAL.Enitites.Identity
{
    public class AppRoleClaim : IdentityRoleClaim<string>
    {
        public virtual AppRole Role { get; set; }
    }

}
