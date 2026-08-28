namespace Soundcloud_Clone.DAL.Enitites.Identity;

public class CommentEntity : BaseEntity
{
    public double? TimeCode { get; set; }
    public string CommentText { get; set; } = string.Empty;

    public int AuthorId { get; set; }
    public UserEntity Author { get; set; }
    
    public int SongId { get; set; }
    public SongEntity Song{ get; set; }
}