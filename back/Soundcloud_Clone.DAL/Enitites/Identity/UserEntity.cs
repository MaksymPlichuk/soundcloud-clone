using Microsoft.AspNetCore.Identity;

namespace Soundcloud_Clone.DAL.Enitites.Identity;

public class UserEntity : IdentityUser<string>
{
    public string? Image { get; set; }
    public List<CommentEntity> Comments = [];
    public List<SongEntity> Songs = [];
    public List<PlaylistEntity> Playlists = [];

    public virtual ICollection<AppUserRole> Claims { get; set; }
    public virtual ICollection<AppUserLogin> Logins { get; set; }
    public virtual ICollection<AppUserToken> Tokens { get; set; }
    public virtual ICollection<AppUserRole> UserRoles { get; set; }
}