using Microsoft.AspNetCore.Mvc;
using Soundcloud_Clone.API.Extensions;
using Soundcloud_Clone.API.Services;
using Soundcloud_Clone.BLL.Dtos.Album;
using Soundcloud_Clone.BLL.Dtos.Song;

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

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var resp = await _service.GetAllAsync();
        return this.GetAction(resp);
    }

    [HttpGet("by-id/{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var resp = await _service.GetByIdAsync(id);
        return this.GetAction(resp);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromForm] CreateAlbumDto song)
    {
        var resp = await _service.CreateAsync(song);
        return this.GetAction(resp);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromForm] UpdateAlbumDto song)
    {
        var resp = await _service.UpdateAsync(song);
        return this.GetAction(resp);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(int id)
    {
        var resp = await _service.DeleteAsync(id);
        return this.GetAction(resp);
    }
}