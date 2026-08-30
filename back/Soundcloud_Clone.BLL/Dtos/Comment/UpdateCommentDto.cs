using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Soundcloud_Clone.BLL.Dtos.Comment
{
    public class UpdateCommentDto
    {
        [Required]
        public int Id { get; set; }
        public double? TimeCode { get; set; }
        [Required]
        public string CommentText { get; set; } = string.Empty;

        public int AuthorId { get; set; } //підігнати під юзера

        [Required]
        public int SongId { get; set; }
    }
}
