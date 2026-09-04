using Soundcloud_Clone.BLL.Dtos.Auth;
using System;
using System.Collections.Generic;
using System.Text;

namespace Soundcloud_Clone.BLL.Dtos.Song
{
    public class SongForInfo
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public double Length { get; set; }
        public string SongFile { get; set; }
        public string? Image { get; set; }
        public UserForInfoDto Artist { get; set; }

    }
}
