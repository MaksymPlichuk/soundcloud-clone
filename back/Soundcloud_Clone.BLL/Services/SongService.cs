using Microsoft.EntityFrameworkCore;
using Soundcloud_Clone.BLL.Dtos.Song;
using Soundcloud_Clone.BLL.Mapperly;
using Soundcloud_Clone.BLL.Services;
using Soundcloud_Clone.DAL.Enitites;
using Soundcloud_Clone.DAL.Repositories;

/// <summary>
/// Service for managing songs.
/// Handles CRUD operations and data transformations between entities and DTOs.
/// </summary>
public class SongService : ISongService
{
    private readonly SongRepository _repository;
    private readonly MapperProfile _mapper;

    /// <summary>
    /// Initializes a new instance of the SongService class.
    /// </summary>
    /// <param name="repository">The song repository for database operations</param>
    /// <param name="mapper">The mapper profile for entity/DTO conversions</param>
    public SongService(SongRepository repository, MapperProfile mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    /// <summary>
    /// Retrieves all songs from the database.
    /// </summary>
    /// <returns>A ServiceResponse containing a list of songs or a failure message if no songs exist</returns>
    public async Task<ServiceResponse> GetAllAsync()
    {
        // Fetch all songs from the database and convert to list
        List<SongEntity> entities = await _repository.GetAll().ToListAsync();
        
        // Return failure response if no songs found
        if (entities.Count == 0) { return ServiceResponse.Failure("No songs found"); }

        // Map entities to DTOs for API response
        var dtos = _mapper.ListSongsToDto(entities);
        return ServiceResponse.Success($"Found {entities.Count} songs", dtos);
    }

    /// <summary>
    /// Retrieves a song entity by its ID from the database.
    /// This is a private helper method used internally by other service methods.
    /// </summary>
    /// <param name="id">The ID of the song to retrieve</param>
    /// <returns>The song entity if found; otherwise null</returns>
    private async Task<SongEntity?> GetByIdEntityAsync(int id)
    {
        return await _repository.GetByIdAsync(id);
    }

    /// <summary>
    /// Retrieves a single song by its ID and returns it as a DTO.
    /// </summary>
    /// <param name="id">The ID of the song to retrieve</param>
    /// <returns>A ServiceResponse containing the song DTO or a failure message if not found</returns>
    public async Task<ServiceResponse> GetByIdAsync(int id)
    {
        // Retrieve song from database
        var entity = await _repository.GetByIdAsync(id);
        
        // Return failure if song does not exist
        if (entity == null) { return ServiceResponse.Failure($"Song with id: {id} not found!"); }

        // Map entity to DTO and return success response
        SongDto dto = _mapper.SongToDto(entity);
        return ServiceResponse.Success($"Song with id: {id}", dto);
    }

    /// <summary>
    /// Creates a new song in the database.
    /// </summary>
    /// <param name="dto">The DTO containing song creation data</param>
    /// <returns>A ServiceResponse with the created song or an error message on failure</returns>
    public async Task<ServiceResponse> CreateAsync(CreateSongDto dto)
    {
        // Map the DTO to entity
        SongEntity entity = _mapper.CreateSongToEntity(dto);
        
        try
        {
            // Attempt to save the entity to the database
            await _repository.CreateAsync(entity);
        }
        catch (Exception ex)
        {
            // Return error message if creation fails
            return ServiceResponse.Failure(ex.Message);
        }
        
        // Return success response with the newly created song
        return ServiceResponse.Success($"Song {entity.Name} created!", _mapper.SongToDto(entity));
    }

    /// <summary>
    /// Updates an existing song in the database.
    /// </summary>
    /// <param name="dto">The DTO containing updated song data with the ID</param>
    /// <returns>A ServiceResponse with the updated song or an error message</returns>
    public async Task<ServiceResponse> UpdateAsync(UpdateSongDto dto)
    {
        // Retrieve the existing song entity by ID
        var entity = await GetByIdEntityAsync(dto.Id);
        if (entity == null)
        {
            // Return failure if song does not exist
            return ServiceResponse.Failure($"Song with id {dto.Id} not found!");
        }

        // Store original name for the response message
        string oldName = entity.Name;
        
        // Apply updates from DTO to entity
        _mapper.UpdateSong(dto, entity);

        // Persist changes to database
        bool upRes = await _repository.UpdateAsync(entity);

        // Return appropriate response based on update result
        if (!upRes) return ServiceResponse.Failure("Update failiure");
        return ServiceResponse.Success($"Song {oldName} successfully updated!", _mapper.SongToDto(entity));
    }

    /// <summary>
    /// Deletes a song from the database by its ID.
    /// </summary>
    /// <param name="id">The ID of the song to delete</param>
    /// <returns>A ServiceResponse with confirmation message or error</returns>
    public async Task<ServiceResponse> DeleteAsync(int id)
    {
        // Retrieve the song entity to get its name for the response
        var entity = await GetByIdEntityAsync(id);
        if (entity == null) return ServiceResponse.Failure($"Song with id {id} not found!");

        // Attempt to delete from database
        bool res = await _repository.DeleteAsync(id);
        if (!res) return ServiceResponse.Failure("Deletion fail");

        // Return success response with deleted song name
        return ServiceResponse.Success($"Song {entity.Name} deleted");
    }
}