
using System.Collections.Generic;
using System.Threading.Tasks;
using Soundcloud_Clone.BLL.Dtos.Comment;

namespace Soundcloud_Clone.BLL.Services
{
    public interface ICommentService
    {
        Task<ServiceResponse> GetAllAsync();
        Task<ServiceResponse> GetByIdAsync(int id);
        Task<ServiceResponse> CreateAsync(CreateCommentDto dto);
        Task<ServiceResponse> UpdateAsync(UpdateCommentDto dto);
        Task<ServiceResponse> DeleteAsync(int id);
    }
}
