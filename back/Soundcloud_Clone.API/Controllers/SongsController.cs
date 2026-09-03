using Microsoft.AspNetCore.Mvc;
using Soundcloud_Clone.API.Extensions;
using Soundcloud_Clone.BLL.Dtos.Song;
using Soundcloud_Clone.BLL.Services;

namespace Soundcloud_Clone.API.Controllers
{
    /// <summary>
    /// API controller for managing songs.
    /// Provides endpoints for CRUD operations on songs.
    /// All responses are handled through the GetAction extension method which converts ServiceResponse to HTTP responses.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class SongsController : ControllerBase
    {
        private readonly ISongService _service;

        /// <summary>
        /// Initializes a new instance of the SongsController class.
        /// </summary>
        /// <param name="service">The song service for business logic operations</param>
        public SongsController(ISongService service)
        {
            _service = service;
        }

        /// <summary>
        /// Retrieves all songs.
        /// </summary>
        /// <returns>HTTP 200 with list of songs or HTTP 404 if no songs exist</returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            // Call service to get all songs
            var resp = await _service.GetAllAsync();
            // Convert ServiceResponse to HTTP response
            return this.GetAction(resp);
        }

        /// <summary>
        /// Retrieves a specific song by its ID.
        /// </summary>
        /// <param name="id">The ID of the song to retrieve</param>
        /// <returns>HTTP 200 with the requested song or HTTP 404 if not found</returns>
        [HttpGet("by-id/{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            // Call service to get song by ID
            var resp = await _service.GetByIdAsync(id);
            // Convert ServiceResponse to HTTP response
            return this.GetAction(resp);
        }

        /// <summary>
        /// Creates a new song.
        /// </summary>
        /// <param name="song">The song data in form format (includes file uploads)</param>
        /// <returns>HTTP 201 with the created song or HTTP 400 on validation/creation failure</returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateSongDto song)
        {
            // Call service to create a new song
            var resp = await _service.CreateAsync(song);
            // Convert ServiceResponse to HTTP response
            return this.GetAction(resp);
        }

        /// <summary>
        /// Updates an existing song.
        /// </summary>
        /// <param name="song">The updated song data in form format (includes file uploads)</param>
        /// <returns>HTTP 200 with the updated song or HTTP 404/400 on failure</returns>
        [HttpPut]
        public async Task<IActionResult> Update([FromForm] UpdateSongDto song)
        {
            // Call service to update the song
            var resp = await _service.UpdateAsync(song);
            // Convert ServiceResponse to HTTP response
            return this.GetAction(resp);
        }

        /// <summary>
        /// Deletes a song by its ID.
        /// </summary>
        /// <param name="id">The ID of the song to delete</param>
        /// <returns>HTTP 200 on successful deletion or HTTP 404 if song not found</returns>
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            // Call service to delete the song
            var resp = await _service.DeleteAsync(id);
            // Convert ServiceResponse to HTTP response
            return this.GetAction(resp);
        }
    }
}
