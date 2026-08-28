using Microsoft.AspNetCore.Identity;


namespace Soundcloud_Clone.DAL.Enitites.Identity
{
    public class AppUserLogin : IdentityUserLogin<string>
    {
        public virtual UserEntity User { get; set; }
    }
}
