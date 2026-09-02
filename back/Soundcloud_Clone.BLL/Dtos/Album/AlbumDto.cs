using Soundcloud_Clone.BLL.Dtos.Auth;
using Soundcloud_Clone.BLL.Dtos.Song;
using Soundcloud_Clone.DAL.Enitites;
using Soundcloud_Clone.DAL.Enitites.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace Soundcloud_Clone.BLL.Dtos.Album
{
    public class AlbumDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public UserForInfoDto Author { get; set; }
        public List<SongForInfo> Songs { get; set; } = [];
        public string? Image { get; set; }
    }
}
