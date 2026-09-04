using Soundcloud_Clone.BLL.Dtos.Album;
using Soundcloud_Clone.BLL.Dtos.Auth;
using Soundcloud_Clone.BLL.Dtos.Comment;
using Soundcloud_Clone.DAL.Enitites;
using Soundcloud_Clone.DAL.Enitites.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Soundcloud_Clone.BLL.Dtos.Song
{
    public class SongDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public double Length { get; set; }
        public string SongFile { get; set; }

        public string? Image { get; set; }

        public UserForInfoDto Artist { get; set; }

        public List<AlbumDto> Albums { get; set; } = [];
        public List<CommentDto> Comments { get; set; } = [];
    }
}
