using System.Collections.Generic;
using System.Threading.Tasks;
using Soundcloud_Clone.BLL.Dtos.Comment;

namespace Soundcloud_Clone.BLL.Services
{
    public interface ICommentService
    {
        Task<IEnumerable<CommentDto>> GetAllAsync();
        Task<CommentDto?> GetByIdAsync(int id);
        Task<CommentDto> CreateAsync(CreateCommentDto dto);
        Task<bool> UpdateAsync(int id, UpdateCommentDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
