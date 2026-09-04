using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Soundcloud_Clone.BLL.Dtos.Song
{
    public class UpdateSongDto
    {
        [Required]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public double Length { get; set; } //переобити під файл

        public IFormFile? Image { get; set; }
        public IFormFile SongFile { get; set; } //обов'язково новий файл при оновленні

        public int ArtistId { get; set; }

        public List<int> AlbumIds { get; set; } = [];
        public List<int> CommentIds { get; set; } = [];
    }
}
