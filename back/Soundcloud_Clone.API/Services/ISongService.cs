using System.Collections.Generic;
using System.Threading.Tasks;
using Soundcloud_Clone.BLL.Dtos.Song;

namespace Soundcloud_Clone.API.Services
{
    public interface ISongService
    {
        Task<IEnumerable<SongDto>> GetAllAsync();
        Task<SongDto?> GetByIdAsync(int id);
        Task<SongDto> CreateAsync(CreateSongDto dto);
        Task<bool> UpdateAsync(int id, UpdateSongDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
