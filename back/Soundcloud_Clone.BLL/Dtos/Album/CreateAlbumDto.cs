using Soundcloud_Clone.BLL.Dtos.Auth;
using Soundcloud_Clone.BLL.Dtos.Song;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace Soundcloud_Clone.BLL.Dtos.Album
{
    public class CreateAlbumDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }

        [Required]
        public int AuthorId { get; set; }

        public List<int> SongIds { get; set; } = [];

        public IFormFile? Image { get; set; }
    }
}
