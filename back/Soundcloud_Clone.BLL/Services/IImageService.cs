using Microsoft.AspNetCore.Http;

namespace Soundcloud_Clone.API.Services;

public interface IImageService
{
    Task<Guid> SaveAlbumImageAsync(IFormFile image);
    Task DeleteAlbumImageAsync(Guid imageId);
}