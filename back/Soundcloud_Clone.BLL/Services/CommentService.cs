using Soundcloud_Clone.BLL.Dtos.Comment;
using Soundcloud_Clone.BLL.Mapperly;
using Soundcloud_Clone.DAL.Enitites;
using Soundcloud_Clone.DAL.Enitites.Identity;
using Soundcloud_Clone.DAL.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Soundcloud_Clone.BLL.Services
{
    public class CommentService : ICommentService
    {
        private readonly ISongRepository _songRepository;
        private readonly GenericRepository<CommentEntity> _commentRepository;
        private readonly MapperProfile _mapper = new();

        public CommentService(ISongRepository songRepository, GenericRepository<CommentEntity> commentRepository)
        {
            _songRepository = songRepository;
            _commentRepository = commentRepository;
        }

        public async Task<CommentDto> CreateAsync(CreateCommentDto dto)
        {
            var entity = _mapper.CreateCommentToEntity(dto);
            await _commentRepository.CreateAsync(entity);
            return _mapper.CommentToDto(entity);
        }

        public Task<bool> DeleteAsync(int id)
        {
            return _commentRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<CommentDto>> GetAllAsync()
        {
            var entities = _commentRepository.GetAll().ToList();
            return _mapper.ListCommentsToDto(entities);
        }

        public async Task<CommentDto?> GetByIdAsync(int id)
        {
            var entity = await _commentRepository.GetByIdAsync(id);
            if (entity is null) return null;
            return _mapper.CommentToDto(entity);
        }

        public async Task<bool> UpdateAsync(int id, UpdateCommentDto dto)
        {
            var existing = await _commentRepository.GetByIdAsync(id);
            if (existing is null) return false;
            _mapper.UpdateComment(dto, existing);
            existing.Id = id;
            return await _commentRepository.UpdateAsync(existing);
        }
    }
}
