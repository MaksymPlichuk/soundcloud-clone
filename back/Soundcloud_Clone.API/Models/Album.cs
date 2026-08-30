namespace Soundcloud_Clone.API.Models;

public class Album
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }

    public int AuthorId { get; set; }
}