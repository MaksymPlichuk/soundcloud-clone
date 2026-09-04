using Microsoft.AspNetCore.Mvc;
using Soundcloud_Clone.API.Extensions;
using Soundcloud_Clone.API.Infrastracture;
using Soundcloud_Clone.BLL.Dtos.Song;
using Soundcloud_Clone.BLL.Services;


namespace Soundcloud_Clone.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SongsController : ControllerBase
    {
        private readonly ISongService _service;
        private string _basePath;
        private string _subPath;

        private string _fileSongPath;

        public SongsController(ISongService service, IWebHostEnvironment env)
        {
            _service = service;
            _basePath = Path.Combine(env.ContentRootPath, StaticFilesSettings.ImageStoragePath);
            _subPath = StaticFilesSettings.SongCoverPath;
            _fileSongPath = StaticFilesSettings.SongStoragePath;

            _fileSongPath = Path.Combine(env.ContentRootPath, _fileSongPath);
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
        public async Task<IActionResult> Create([FromForm] CreateSongDto song)
        {
            var resp = await _service.CreateAsync(song, _basePath, _subPath, _fileSongPath);
            return this.GetAction(resp);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromForm] UpdateSongDto song)
        {
            var resp = await _service.UpdateAsync(song, _basePath, _subPath, _fileSongPath);
            return this.GetAction(resp);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var resp = await _service.DeleteAsync(id, _basePath, _fileSongPath);
            return this.GetAction(resp);
        }
    }
}
