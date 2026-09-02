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
    private readonly IImageService _imageService;

    public AlbumService(
        AlbumRepository repository,
        SongRepository songRepository,
        MapperProfile mapper,
        IImageService imageService)
    {
        _repository = repository;
        _songRepository = songRepository;
        _mapper = mapper;
        _imageService = imageService;
    }

    public async Task<ServiceResponse> GetAllAsync()
    {
        var entities = await _repository
            .GetAll()
            .ToListAsync();

        if (entities.Count == 0)
        {
            return ServiceResponse.Failure("No entities found");
        }

        var dtos = _mapper.ListAlbumsToDto(entities);

        return ServiceResponse.Success(
            $"Found {entities.Count} albums",
            dtos);
    }

    private async Task<AlbumEntity?> GetByIdEntityAsync(int id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task<ServiceResponse> GetByIdAsync(int id)
    {
        var entity = await _repository.GetByIdAsync(id);

        if (entity == null)
        {
            return ServiceResponse.Failure(
                $"Album with id: {id} not found!");
        }

        var dto = _mapper.AlbumToDto(entity);

        return ServiceResponse.Success(
            $"Album with id: {id}",
            dto);
    }

    public async Task<ServiceResponse> CreateAsync(CreateAlbumDto dto)
    {
        try
        {
            var entity = _mapper.CreateAlbumToEntity(dto);

            /*
             * Load songs from database.
             */
            if (dto.SongIds.Count > 0)
            {
                var songs = await _songRepository
                    .GetAll()
                    .Where(song => dto.SongIds.Contains(song.Id))
                    .ToListAsync();

                /*
                 * Make sure every requested song exists.
                 */
                if (songs.Count != dto.SongIds.Distinct().Count())
                {
                    return ServiceResponse.Failure(
                        "One or more selected songs were not found.");
                }

                entity.Songs = songs;
            }

            /*
             * Save image.
             */
            if (dto.Image is not null)
            {
                entity.Image =
                    await _imageService.SaveAlbumImageAsync(dto.Image);
            }

            await _repository.CreateAsync(entity);

            /*
             * Reload entity with Author and Songs.
             */
            var createdEntity =
                await _repository.GetByIdAsync(entity.Id);

            if (createdEntity is null)
            {
                return ServiceResponse.Failure(
                    "Album was created, but could not be loaded.");
            }

            return ServiceResponse.Success(
                $"Album {entity.Name} created!",
                _mapper.AlbumToDto(createdEntity));
        }
        catch (Exception ex)
        {
            return ServiceResponse.Failure(ex.Message);
        }
    }

    public async Task<ServiceResponse> UpdateAsync(UpdateAlbumDto dto)
    {
        var entity = await GetByIdEntityAsync(dto.Id);

        if (entity == null)
        {
            return ServiceResponse.Failure(
                $"Album with id {dto.Id} not found!");
        }

        var oldName = entity.Name;

        try
        {
            /*
             * Update image only when a new image was provided.
             */
            if (dto.Image is not null)
            {
                if (entity.Image.HasValue)
                {
                    await _imageService.DeleteAlbumImageAsync(
                        entity.Image.Value);
                }

                entity.Image =
                    await _imageService.SaveAlbumImageAsync(dto.Image);
            }

            /*
             * Replace album songs.
             */
            var songs = await _songRepository
                .GetAll()
                .Where(song => dto.SongIds.Contains(song.Id))
                .ToListAsync();

            if (songs.Count != dto.SongIds.Distinct().Count())
            {
                return ServiceResponse.Failure(
                    "One or more selected songs were not found.");
            }

            entity.Songs = songs;

            /*
             * Mapperly updates:
             * Name
             * Description
             * AuthorId
             *
             * Image and Songs are ignored by Mapperly.
             */
            _mapper.UpdateAlbum(dto, entity);

            var result = await _repository.UpdateAsync(entity);

            if (!result)
            {
                return ServiceResponse.Failure(
                    "Update failure");
            }

            /*
             * Reload entity with all relations.
             */
            var updatedEntity =
                await _repository.GetByIdAsync(entity.Id);

            if (updatedEntity is null)
            {
                return ServiceResponse.Failure(
                    "Album was updated, but could not be loaded.");
            }

            return ServiceResponse.Success(
                $"Album {oldName} successfully updated!",
                _mapper.AlbumToDto(updatedEntity));
        }
        catch (Exception ex)
        {
            return ServiceResponse.Failure(ex.Message);
        }
    }

    public async Task<ServiceResponse> DeleteAsync(int id)
    {
        var album = await GetByIdEntityAsync(id);

        if (album == null)
        {
            return ServiceResponse.Failure(
                $"Album with id {id} not found");
        }

        var result = await _repository.DeleteAsync(id);

        if (!result)
        {
            return ServiceResponse.Failure(
                "Deletion failed");
        }

        return ServiceResponse.Success(
            $"Album {album.Name} deleted");
    }
}