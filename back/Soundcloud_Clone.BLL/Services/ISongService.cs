using System.Collections.Generic;
using System.Threading.Tasks;
using Soundcloud_Clone.BLL.Dtos.Song;

namespace Soundcloud_Clone.BLL.Services
{
    public interface ISongService
    {
        Task<ServiceResponse> GetAllAsync();
        Task<ServiceResponse> GetByIdAsync(int id);
        Task<ServiceResponse> CreateAsync(CreateSongDto dto, string basePath, string subPath);
        Task<ServiceResponse> UpdateAsync(UpdateSongDto dto, string basePath, string subPath);
        Task<ServiceResponse> DeleteAsync(int id, string basePath);
    }
}
