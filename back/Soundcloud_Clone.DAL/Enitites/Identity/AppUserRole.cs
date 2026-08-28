using Microsoft.AspNetCore.Identity;


namespace Soundcloud_Clone.DAL.Enitites.Identity
{
    public class AppUserRole : IdentityUserRole<int>
    {
        public virtual UserEntity User { get; set; }
        public virtual AppRole Role { get; set; }
    }
}
