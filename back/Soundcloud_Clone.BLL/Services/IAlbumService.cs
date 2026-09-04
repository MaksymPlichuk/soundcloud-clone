using Soundcloud_Clone.BLL.Dtos.Album;
using Soundcloud_Clone.BLL.Services;
using Soundcloud_Clone.DAL.Enitites;

namespace Soundcloud_Clone.API.Services;

public interface IAlbumService
{
    Task<ServiceResponse> GetAllAsync();
    Task<ServiceResponse> GetByIdAsync(int id);
    Task<ServiceResponse> CreateAsync(CreateAlbumDto dto, string basePath, string subPath);
    Task<ServiceResponse> UpdateAsync(UpdateAlbumDto dto, string basePath, string subPath);
    Task<ServiceResponse> DeleteAsync(int id, string basePath);
}