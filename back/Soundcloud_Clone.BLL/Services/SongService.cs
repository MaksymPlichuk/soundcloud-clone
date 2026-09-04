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
    private readonly ImageService _imageService;

    public SongService(SongRepository repository, MapperProfile mapper, ImageService imageService)
    {
        _repository = repository;
        _mapper = mapper;
        _imageService = imageService;
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

    public async Task<ServiceResponse> CreateAsync(CreateSongDto dto, string basePath, string subPath)
    {
        SongEntity entity = _mapper.CreateSongToEntity(dto);
        if (dto.Image != null)
        {
            var resp = await _imageService.CreateImageAsync(dto.Image, basePath, subPath);
            if (!resp.IsSuccess) return resp;

            entity.Image = resp.Payload.ToString();
        }

        try
        {
            await _repository.CreateAsync(entity);
        }
        catch (Exception ex)
        {
            if (entity.Image != null) { _imageService.DeleteImage(basePath, entity.Image); }
            return ServiceResponse.Failure(ex.Message);
        }

        var fullEntity = await _repository.GetByIdAsync(entity.Id);

        return ServiceResponse.Success($"Song {entity.Name} created!", _mapper.SongToDto(fullEntity));
    }

    public async Task<ServiceResponse> UpdateAsync(UpdateSongDto dto, string basePath, string subPath)
    {
        var entity = await GetByIdEntityAsync(dto.Id);
        if (entity == null)
        {
            return ServiceResponse.Failure($"Song with id {dto.Id} not found!");
        }

        string oldName = entity.Name;
        _mapper.UpdateSong(dto, entity);

        string newImageName = "";
        if (dto.Image != null)
        {
            if (entity.Image != null) { _imageService.DeleteImage(basePath, entity.Image); }

            var resp = await _imageService.CreateImageAsync(dto.Image, basePath, subPath);
            if (!resp.IsSuccess) return resp;

            newImageName = resp.Payload.ToString();
            entity.Image = resp.Payload.ToString();
        }

        bool upRes = await _repository.UpdateAsync(entity);

        if (!upRes)
        {
            if (dto.Image != null) { _imageService.DeleteImage(basePath, newImageName); }
            return ServiceResponse.Failure("Update failiure");
        }

        return ServiceResponse.Success($"Song {oldName} successfully updated!", _mapper.SongToDto(entity));
    }

    public async Task<ServiceResponse> DeleteAsync(int id, string basePath)
    {
        var entity = await GetByIdEntityAsync(id);
        if (entity == null) return ServiceResponse.Failure($"Song with id {id} not found!");

        if (entity.Image != null) { _imageService.DeleteImage(basePath, entity.Image); }

        bool res = await _repository.DeleteAsync(id);
        if (!res) return ServiceResponse.Failure("Deletion fail");

        return ServiceResponse.Success($"Song {entity.Name} deleted");
    }
}