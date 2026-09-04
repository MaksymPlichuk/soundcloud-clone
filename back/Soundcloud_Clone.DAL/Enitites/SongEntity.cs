using Soundcloud_Clone.DAL.Enitites.Identity;

namespace Soundcloud_Clone.DAL.Enitites;

public class SongEntity : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public double Length { get; set; }
    
    public string? Image { get; set; }

    public string SongFile { get; set; } = string.Empty;
    public int ArtistId { get; set; }
    public UserEntity Artist { get; set; }

    public List<AlbumEntity> Albums { get; set; } = [];
    public List<CommentEntity> Comments { get; set; } = [];
}