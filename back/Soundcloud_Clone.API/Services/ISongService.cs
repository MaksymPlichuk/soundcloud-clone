using System.Collections.Generic;
using System.Threading.Tasks;
using Soundcloud_Clone.API.Models;

namespace Soundcloud_Clone.API.Services
{
    public interface ISongService
    {
        Task<IEnumerable<Song>> GetAllAsync();
        Task<Song?> GetByIdAsync(int id);
        Task<Song> CreateAsync(Song song);
        Task<bool> UpdateAsync(int id, Song song);
        Task<bool> DeleteAsync(int id);
    }
}
