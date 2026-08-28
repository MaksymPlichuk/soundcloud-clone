using Microsoft.AspNetCore.Identity;


namespace Soundcloud_Clone.DAL.Enitites.Identity
{
    public class AppUserLogin : IdentityUserLogin<int>
    {
        public virtual UserEntity User { get; set; }
    }
}
