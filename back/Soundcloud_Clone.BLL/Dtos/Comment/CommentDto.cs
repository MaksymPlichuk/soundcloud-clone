using Soundcloud_Clone.BLL.Dtos.Auth;
using Soundcloud_Clone.BLL.Dtos.Song;
using Soundcloud_Clone.DAL.Enitites;
using Soundcloud_Clone.DAL.Enitites.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Soundcloud_Clone.BLL.Dtos.Comment
{
    public class CommentDto
    {
        public double? TimeCode { get; set; }
        public string CommentText { get; set; } = string.Empty;

        public UserForInfoDto Author { get; set; }

        public SongForInfo Song { get; set; }
    }
}
