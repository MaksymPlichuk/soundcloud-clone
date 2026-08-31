using Microsoft.AspNetCore.Mvc;
using Soundcloud_Clone.API.Models;
using Soundcloud_Clone.API.Services;

namespace Soundcloud_Clone.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AlbumsController : ControllerBase
{
    private readonly IAlbumService _service;

    public AlbumsController(IAlbumService service)
    {
        _service = service;
    }

    // GET: api/albums
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var albums = await _service.GetAllAsync();

        return Ok(albums);
    }

    // GET: api/albums/1
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var album = await _service.GetByIdAsync(id);

        if (album is null)
            return NotFound();

        return Ok(album);
    }

    // POST: api/albums
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Album album)
    {
        var created = await _service.CreateAsync(album);

        return CreatedAtAction(
            nameof(GetById),
            new { id = created.Id },
            created
        );
    }

    // PUT: api/albums/1
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(
        int id,
        [FromBody] Album album)
    {
        var updated = await _service.UpdateAsync(id, album);

        if (!updated)
            return NotFound();

        return NoContent();
    }

    // DELETE: api/albums/1
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _service.DeleteAsync(id);

        if (!deleted)
            return NotFound();

        return NoContent();
    }
}