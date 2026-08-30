using System.Collections.Generic;
using System.Threading.Tasks;
using Soundcloud_Clone.API.Models;
using Soundcloud_Clone.API.Repositories;

namespace Soundcloud_Clone.API.Services
{
    public class SongService : ISongService
    {
        private readonly ISongRepository _repository;

        public SongService(ISongRepository repository)
        {
            _repository = repository;
        }

        public Task<Song> CreateAsync(Song song)
        {
            return _repository.CreateAsync(song);
        }

        public Task<bool> DeleteAsync(int id)
        {
            return _repository.DeleteAsync(id);
        }

        public Task<IEnumerable<Song>> GetAllAsync()
        {
            return _repository.GetAllAsync();
        }

        public Task<Song?> GetByIdAsync(int id)
        {
            return _repository.GetByIdAsync(id);
        }

        public async Task<bool> UpdateAsync(int id, Song song)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing is null) return false;
            song.Id = id;
            return await _repository.UpdateAsync(song);
        }
    }
}
