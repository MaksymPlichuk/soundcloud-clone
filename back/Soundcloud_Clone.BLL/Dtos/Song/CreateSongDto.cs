using Soundcloud_Clone.BLL.Dtos.Album;
using Soundcloud_Clone.BLL.Dtos.Auth;
using Soundcloud_Clone.BLL.Dtos.Comment;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Soundcloud_Clone.BLL.Dtos.Song
{
    public class CreateSongDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        public double Length { get; set; } //переобити під файл

        public string? Image { get; set; }

        public int ArtistId { get; set; }

        public List<int> AlbumIds { get; set; } = [];
        public List<int> CommentIds { get; set; } = [];
    }
}
