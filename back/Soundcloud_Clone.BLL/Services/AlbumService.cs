using Microsoft.EntityFrameworkCore;
using Soundcloud_Clone.API.Repositories;
using Soundcloud_Clone.BLL.Dtos.Album;
using Soundcloud_Clone.BLL.Mapperly;
using Soundcloud_Clone.BLL.Services;
using Soundcloud_Clone.DAL.Enitites;

namespace Soundcloud_Clone.API.Services;

public class AlbumService : IAlbumService
{
    private readonly AlbumRepository _repository;
    private readonly MapperProfile _mapper;
    private readonly IImageService _imageService;

    public AlbumService( AlbumRepository repository, MapperProfile mapper, IImageService imageService)
    {
        _repository = repository;
        _mapper = mapper;
        _imageService = imageService;
    }

    public async Task<ServiceResponse> GetAllAsync()
    {
        List<AlbumEntity> entities = await _repository.GetAll().ToListAsync();
        if (entities.Count == 0) { return ServiceResponse.Failure("No entities found"); }

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

    public async Task<ServiceResponse> CreateAsync(CreateAlbumDto dto)
    {
        AlbumEntity entity = _mapper.CreateAlbumToEntity(dto);

        try
        {
            if (dto.Image is not null)
            {
                entity.Image = await _imageService.SaveAlbumImageAsync(dto.Image);
            }

            await _repository.CreateAsync(entity);
        }
        catch (Exception ex)
        {
            return ServiceResponse.Failure(ex.Message);
        }

        return ServiceResponse.Success(
            $"Album {entity.Name} created!",
            _mapper.AlbumToDto(entity));
    }

    public async Task<ServiceResponse> UpdateAsync(UpdateAlbumDto dto)
    {
        var entity = await GetByIdEntityAsync(dto.Id);

        if (entity == null)
        {
            return ServiceResponse.Failure(
                $"Album with id {dto.Id} not found!");
        }

        string oldName = entity.Name;

        try
        {
            if (dto.Image is not null)
            {
                if (entity.Image.HasValue)
                {
                    await _imageService.DeleteAlbumImageAsync(
                        entity.Image.Value
                    );
                }

                entity.Image =
                    await _imageService.SaveAlbumImageAsync(dto.Image);
            }

            _mapper.UpdateAlbum(dto, entity);

            bool result = await _repository.UpdateAsync(entity);

            if (!result)
            {
                return ServiceResponse.Failure("Update failure");
            }
        }
        catch (Exception ex)
        {
            return ServiceResponse.Failure(ex.Message);
        }

        return ServiceResponse.Success(
            $"Album {oldName} successfully updated!",
            _mapper.AlbumToDto(entity));
    }

    public async Task<ServiceResponse> DeleteAsync(int id)
    {
        var album = await GetByIdEntityAsync(id);
        if (album == null) return ServiceResponse.Failure($"Album wth id {id} not found");

        bool res = await _repository.DeleteAsync(id);
        if (!res) return ServiceResponse.Failure("Deletion fail");

        return ServiceResponse.Success($"Album {album.Name} deleted");
    }
}