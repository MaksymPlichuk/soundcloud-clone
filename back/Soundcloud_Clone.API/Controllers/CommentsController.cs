using Microsoft.AspNetCore.Mvc;
using Soundcloud_Clone.API.Extensions;
using Soundcloud_Clone.BLL.Dtos.Comment;
using Soundcloud_Clone.BLL.Services;
using System.Threading.Tasks;

namespace Soundcloud_Clone.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentService _service;

        public CommentsController(ICommentService service)
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
        public async Task<IActionResult> Create([FromBody] CreateCommentDto comment)
        {
            var resp = await _service.CreateAsync(comment);
            return this.GetAction(resp);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateCommentDto comment)
        {
            var resp = await _service.UpdateAsync(comment);
            return this.GetAction(resp);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var resp = await _service.DeleteAsync(id);
            return this.GetAction(resp);
        }
    }
}
