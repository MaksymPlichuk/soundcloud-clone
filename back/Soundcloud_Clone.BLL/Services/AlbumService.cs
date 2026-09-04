using Microsoft.EntityFrameworkCore;
using Soundcloud_Clone.API.Repositories;
using Soundcloud_Clone.BLL.Dtos.Album;
using Soundcloud_Clone.BLL.Mapperly;
using Soundcloud_Clone.BLL.Services;
using Soundcloud_Clone.DAL.Enitites;
using Soundcloud_Clone.DAL.Repositories;


namespace Soundcloud_Clone.API.Services;

public class AlbumService : IAlbumService
{
    private readonly AlbumRepository _repository;
    private readonly SongRepository _songRepository;
    private readonly MapperProfile _mapper;
    private readonly ImageService _imageService;

    public AlbumService(
        AlbumRepository repository,
        SongRepository songRepository,
        MapperProfile mapper,
        ImageService imageService)
    {
        _repository = repository;
        _songRepository = songRepository;
        _mapper = mapper;
        _imageService = imageService;
    }

    public async Task<ServiceResponse> GetAllAsync()
    {
        List<AlbumEntity> entities = await _repository.GetAll().ToListAsync();
        if (entities.Count == 0) { return ServiceResponse.Failure("No albums found"); }

        var dtos = _mapper.ListAlbumsToDto(entities);
        return ServiceResponse.Success($"Found {entities.Count} albums", dtos);
    }

    private async Task<AlbumEntity?> GetByIdEntityAsync(int id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task<ServiceResponse> GetByIdAsync(int id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null) { return ServiceResponse.Failure($"Album with id: {id} not found!"); }

        AlbumDto dto = _mapper.AlbumToDto(entity);
        return ServiceResponse.Success($"Album with id: {id}", dto);
    }

    public async Task<ServiceResponse> CreateAsync(CreateAlbumDto dto, string basePath, string subPath)
    {
        AlbumEntity entity = _mapper.CreateAlbumToEntity(dto);

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
        return ServiceResponse.Success($"Album {entity.Name} created!", _mapper.AlbumToDto(fullEntity));
    }

    public async Task<ServiceResponse> UpdateAsync(UpdateAlbumDto dto, string basePath, string subPath)
    {
        var entity = await GetByIdEntityAsync(dto.Id);
        if (entity == null)
        {
            return ServiceResponse.Failure($"Album with id {dto.Id} not found!");
        }

        string oldName = entity.Name;
        _mapper.UpdateAlbum(dto, entity);

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
            return ServiceResponse.Failure("Update failure");
        }

        return ServiceResponse.Success($"Album {oldName} successfully updated!", _mapper.AlbumToDto(entity));
    }

    public async Task<ServiceResponse> DeleteAsync(int id, string basePath)
    {
        var entity = await GetByIdEntityAsync(id);
        if (entity == null) return ServiceResponse.Failure($"Album with id {id} not found!");

        if (entity.Image != null) { _imageService.DeleteImage(basePath, entity.Image); }

        bool res = await _repository.DeleteAsync(id);
        if (!res) return ServiceResponse.Failure("Deletion fail");

        return ServiceResponse.Success($"Album {entity.Name} deleted");
    }
}