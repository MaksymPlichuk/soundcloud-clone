using Microsoft.EntityFrameworkCore;
using Soundcloud_Clone.BLL.Dtos.Song;
using Soundcloud_Clone.BLL.Mapperly;
using Soundcloud_Clone.BLL.Services;
using Soundcloud_Clone.DAL.Enitites;
using Soundcloud_Clone.DAL.Repositories;

public class SongService : ISongService
{
    private readonly SongRepository _repository;
    private readonly MapperProfile _mapper;

    public SongService(SongRepository repository, MapperProfile mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ServiceResponse> GetAllAsync()
    {
        List<SongEntity> entities = await _repository.GetAll().ToListAsync();
        if (entities.Count == 0) { return ServiceResponse.Failure("No songs found"); }

        var dtos = _mapper.ListSongsToDto(entities);
        return ServiceResponse.Success($"Found {entities.Count} songs", dtos);
    }

    private async Task<SongEntity?> GetByIdEntityAsync(int id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task<ServiceResponse> GetByIdAsync(int id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null) { return ServiceResponse.Failure($"Song with id: {id} not found!"); }

        SongDto dto = _mapper.SongToDto(entity);
        return ServiceResponse.Success($"Song with id: {id}", dto);
    }

    public async Task<ServiceResponse> CreateAsync(CreateSongDto dto)
    {
        SongEntity entity = _mapper.CreateSongToEntity(dto);
        try
        {
            await _repository.CreateAsync(entity);
        }
        catch (Exception ex)
        {
            return ServiceResponse.Failure(ex.Message);
        }

        var fullEntity = await _repository.GetByIdAsync(entity.Id); //коли приходить з фронту підвантажує author інкаше помилка

        return ServiceResponse.Success($"Song {entity.Name} created!", _mapper.SongToDto(fullEntity));
    }

    public async Task<ServiceResponse> UpdateAsync(UpdateSongDto dto)
    {
        var entity = await GetByIdEntityAsync(dto.Id);
        if (entity == null)
        {
            return ServiceResponse.Failure($"Song with id {dto.Id} not found!");
        }

        string oldName = entity.Name;
        _mapper.UpdateSong(dto, entity);

        bool upRes = await _repository.UpdateAsync(entity);

        if (!upRes) return ServiceResponse.Failure("Update failiure");
        return ServiceResponse.Success($"Song {oldName} successfully updated!", _mapper.SongToDto(entity));
    }

    public async Task<ServiceResponse> DeleteAsync(int id)
    {
        var entity = await GetByIdEntityAsync(id);
        if (entity == null) return ServiceResponse.Failure($"Song with id {id} not found!");

        bool res = await _repository.DeleteAsync(id);
        if (!res) return ServiceResponse.Failure("Deletion fail");

        return ServiceResponse.Success($"Song {entity.Name} deleted");
    }
}