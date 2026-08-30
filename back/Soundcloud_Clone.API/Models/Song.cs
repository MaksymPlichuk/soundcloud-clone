using System;

namespace Soundcloud_Clone.API.Models
{
    public class Song
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Artist { get; set; } = string.Empty;
        public int DurationSeconds { get; set; }
        public DateTime? ReleaseDate { get; set; }
    }
}
