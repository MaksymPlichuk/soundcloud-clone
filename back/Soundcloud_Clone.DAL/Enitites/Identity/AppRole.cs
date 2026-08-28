using Microsoft.AspNet.Identity.EntityFramework;

namespace Soundcloud_Clone.DAL.Enitites.Identity
{
    public class AppRole : IdentityRole
    {
        public virtual ICollection<AppUserRole> UserRoles { get; set; }
        public virtual ICollection<AppRoleClaim> RoleClaims { get; set; }
    }
}
