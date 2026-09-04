using Microsoft.EntityFrameworkCore;
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

        private readonly CommentRepository _repository;
        private readonly MapperProfile _mapper;

        public CommentService(CommentRepository repository, MapperProfile mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ServiceResponse> GetAllAsync()
        {
            List<CommentEntity> entities = await _repository.GetAll().ToListAsync();
            if (entities.Count == 0) { return ServiceResponse.Failure("No comments found"); }

            var dtos = _mapper.ListCommentsToDto(entities);
            return ServiceResponse.Success($"Found {entities.Count} comments", dtos);
        }

        private async Task<CommentEntity?> GetByIdEntityAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<ServiceResponse> GetByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) { return ServiceResponse.Failure($"Comment with id: {id} not found!"); }

            CommentDto dto = _mapper.CommentToDto(entity);
            return ServiceResponse.Success($"Comment with id: {id}", dto);
        }

        public async Task<ServiceResponse> CreateAsync(CreateCommentDto dto)
        {
            CommentEntity entity = _mapper.CreateCommentToEntity(dto);

            try
            {
                await _repository.CreateAsync(entity);
            }
            catch (Exception ex)
            {
                return ServiceResponse.Failure(ex.Message);
            }

            var fullEntity = await _repository.GetByIdAsync(entity.Id);

            return ServiceResponse.Success("Comment created!", _mapper.CommentToDto(fullEntity));
        }

        public async Task<ServiceResponse> UpdateAsync(UpdateCommentDto dto)
        {
            var entity = await GetByIdEntityAsync(dto.Id);
            if (entity == null)
            {
                return ServiceResponse.Failure($"Comment with id {dto.Id} not found!");
            }

            _mapper.UpdateComment(dto, entity);

            bool upRes = await _repository.UpdateAsync(entity);

            if (!upRes)
            {
                return ServiceResponse.Failure("Update failure");
            }

            var fullEntity = await _repository.GetByIdAsync(entity.Id);
            return ServiceResponse.Success("Comment successfully updated!", _mapper.CommentToDto(fullEntity));
        }

        public async Task<ServiceResponse> DeleteAsync(int id)
        {
            var entity = await GetByIdEntityAsync(id);
            if (entity == null) return ServiceResponse.Failure($"Comment with id {id} not found!");

            bool res = await _repository.DeleteAsync(id);
            if (!res) return ServiceResponse.Failure("Deletion fail");

            return ServiceResponse.Success("Comment deleted");
        }
    }
}
