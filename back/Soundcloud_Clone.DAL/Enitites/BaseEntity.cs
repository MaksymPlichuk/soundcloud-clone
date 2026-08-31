namespace Soundcloud_Clone.DAL.Enitites;

public interface IBaseEntity
{
    public int Id { get; set; }
    public DateTime CreationDate { get; set; }
}

public class BaseEntity : IBaseEntity
{
    public int Id { get; set; }
    public DateTime CreationDate { get; set; } = DateTime.UtcNow;
}