using Soundcloud_Clone.API.Models;

namespace Soundcloud_Clone.API.Repositories;

public interface IAlbumRepository
{
	Task<IEnumerable<Album>> GetAllAsync();

	Task<Album?> GetByIdAsync(int id);

	Task<Album> CreateAsync(Album album);

	Task<bool> UpdateAsync(Album album);

	Task<bool> DeleteAsync(int id);
}