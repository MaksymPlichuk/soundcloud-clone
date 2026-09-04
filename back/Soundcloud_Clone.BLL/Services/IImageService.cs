using Microsoft.AspNetCore.Http;

namespace Soundcloud_Clone.API.Services;

public interface IImageService
{
    Task<string> SaveAlbumImageAsync(IFormFile image);
    Task DeleteAlbumImageAsync(string imageId);
}