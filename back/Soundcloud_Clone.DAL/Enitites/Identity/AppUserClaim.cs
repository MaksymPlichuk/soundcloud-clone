using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Soundcloud_Clone.DAL.Enitites.Identity
{
    public class AppUserClaim : IdentityUserClaim<string>
    {
        public virtual UserEntity User { get; set; }
    }
}
