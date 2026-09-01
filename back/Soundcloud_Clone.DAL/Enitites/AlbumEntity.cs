using Soundcloud_Clone.DAL.Enitites.Identity;

namespace Soundcloud_Clone.DAL.Enitites;

public class AlbumEntity : BaseEntity
{
    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }

    public Guid? Image { get; set; }

    public int AuthorId { get; set; }

    public UserEntity Author { get; set; }

    public List<SongEntity> Songs { get; set; } = [];
}