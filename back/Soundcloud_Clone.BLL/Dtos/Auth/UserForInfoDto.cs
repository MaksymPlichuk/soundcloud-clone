using System;
using System.Collections.Generic;
using System.Text;

namespace Soundcloud_Clone.BLL.Dtos.Auth
{
    public class UserForInfoDto
    {
        public int Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string? Image { get; set; }

    }
}
