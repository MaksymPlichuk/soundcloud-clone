using Soundcloud_Clone.API.Models;

namespace Soundcloud_Clone.API.Services;

public interface IAlbumService
{
    Task<IEnumerable<Album>> GetAllAsync();

    Task<Album?> GetByIdAsync(int id);

    Task<Album> CreateAsync(Album album);

    Task<bool> UpdateAsync(int id, Album album);

    Task<bool> DeleteAsync(int id);
}