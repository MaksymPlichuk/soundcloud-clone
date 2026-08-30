using Soundcloud_Clone.API.Models;
using Soundcloud_Clone.API.Repositories;

namespace Soundcloud_Clone.API.Services;

public class AlbumService : IAlbumService
{
    private readonly IAlbumRepository _repository;

    public AlbumService(IAlbumRepository repository)
    {
        _repository = repository;
    }

    public Task<IEnumerable<Album>> GetAllAsync()
    {
        return _repository.GetAllAsync();
    }

    public Task<Album?> GetByIdAsync(int id)
    {
        return _repository.GetByIdAsync(id);
    }

    public Task<Album> CreateAsync(Album album)
    {
        return _repository.CreateAsync(album);
    }

    public async Task<bool> UpdateAsync(int id, Album album)
    {
        var existing = await _repository.GetByIdAsync(id);

        if (existing is null)
            return false;

        album.Id = id;

        return await _repository.UpdateAsync(album);
    }

    public Task<bool> DeleteAsync(int id)
    {
        return _repository.DeleteAsync(id);
    }
}