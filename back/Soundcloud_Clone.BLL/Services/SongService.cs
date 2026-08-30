using Soundcloud_Clone.API.Models;
using Soundcloud_Clone.API.Repositories;
using Soundcloud_Clone.BLL.Dtos.Song;
using Soundcloud_Clone.BLL.Mapperly;
using Soundcloud_Clone.DAL.Enitites;
using Soundcloud_Clone.DAL.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Soundcloud_Clone.API.Services
{
    public class SongService
    {
        private readonly MapperProfile _mapper;
        private readonly SongRepository _repository;
        public SongService(SongRepository repository, MapperProfile mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<bool> CreateAsync(CreateSongDto song)
        {
            SongEntity songEntity = _mapper.CreateSongToEntity(song);
            
            return  await _repository.CreateAsync(songEntity);
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
