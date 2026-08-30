using System.Collections.Generic;
using System.Threading.Tasks;
using Soundcloud_Clone.API.Models;

namespace Soundcloud_Clone.API.Repositories
{
    public interface ISongRepository
    {
        Task<IEnumerable<Song>> GetAllAsync();
        Task<Song?> GetByIdAsync(int id);
        Task<Song> CreateAsync(Song song);
        Task<bool> UpdateAsync(Song song);
        Task<bool> DeleteAsync(int id);
    }
}
