using System.Collections.Generic;
using System.Threading.Tasks;
using Soundcloud_Clone.DAL.Enitites;

namespace Soundcloud_Clone.DAL.Repositories
{
    public interface ISongRepository
    {
        Task<IEnumerable<SongEntity>> GetAllAsync();
        Task<SongEntity?> GetByIdAsync(int id);
        Task<SongEntity> CreateAsync(SongEntity song);
        Task<bool> UpdateAsync(SongEntity song);
        Task<bool> DeleteAsync(int id);
    }
}
