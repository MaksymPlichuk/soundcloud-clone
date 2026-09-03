using Microsoft.AspNetCore.Http;
using Soundcloud_Clone.BLL.Dtos.Album;
using Soundcloud_Clone.BLL.Dtos.Comment;
using Soundcloud_Clone.BLL.Dtos.Song;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Soundcloud_Clone.BLL.Dtos.Auth
{
    public class RegisterDto
    {
        [Required]
        public string UserName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; }
        public string? Image { get; set; } // змінити на IFormFile

    }
}