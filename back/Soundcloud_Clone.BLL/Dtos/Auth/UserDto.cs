using Soundcloud_Clone.BLL.Dtos.Album;
using Soundcloud_Clone.BLL.Dtos.Comment;
using Soundcloud_Clone.BLL.Dtos.Song;
using System;
using System.Collections.Generic;
using System.Text;

namespace Soundcloud_Clone.BLL.Dtos.Auth
{
    public class UserDto
    {
        public int Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string? Image { get; set; }

        public List<AlbumDto> Albums { get; set; } = [];
        public List<SongDto> Songs { get; set; } = [];
        public List<CommentDto> Comments { get; set; } = [];
    }
}
