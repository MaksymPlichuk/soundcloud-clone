using Soundcloud_Clone.BLL.Dtos.Song;
using Soundcloud_Clone.BLL.Mapperly;
using Soundcloud_Clone.DAL.Enitites;
using Soundcloud_Clone.DAL.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Soundcloud_Clone.BLL.Services
{
    public class SongService : ISongService
    {
        private readonly ISongRepository _repository;
        private readonly MapperProfile _mapper = new();

        public SongService(ISongRepository repository)
        {
            _repository = repository;
        }

        public async Task<SongDto> CreateAsync(CreateSongDto dto)
        {
            var entity = _mapper.CreateSongToEntity(dto);
            var created = await _repository.CreateAsync(entity);
            return _mapper.SongToDto(created);
        }

        public Task<bool> DeleteAsync(int id)
        {
            return _repository.DeleteAsync(id);
        }

        public async Task<IEnumerable<SongDto>> GetAllAsync()
        {
            var entities = await _repository.GetAllAsync();
            return _mapper.ListSongsToDto(entities.ToList());
        }

        public async Task<SongDto?> GetByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity is null) return null;
            return _mapper.SongToDto(entity);
        }

        public async Task<bool> UpdateAsync(int id, UpdateSongDto dto)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing is null) return false;
            _mapper.UpdateSong(dto, existing);
            existing.Id = id;
            return await _repository.UpdateAsync(existing);
        }
    }
}
