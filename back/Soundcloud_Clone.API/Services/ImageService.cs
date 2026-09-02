using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Soundcloud_Clone.API.Infrastracture;


namespace Soundcloud_Clone.API.Services;

public class ImageService : IImageService
{
	private readonly IWebHostEnvironment _environment;

	public ImageService(IWebHostEnvironment environment)
	{
		_environment = environment;
	}

	public async Task<Guid> SaveAlbumImageAsync(IFormFile image)
	{
		if (image.Length == 0)
			throw new ArgumentException("Image is empty.");

		var extension = Path.GetExtension(image.FileName);

		var allowedExtensions = new[]
		{
			".jpg",
			".jpeg",
			".png",
			".webp"
		};

		if (!allowedExtensions.Contains(
				extension,
				StringComparer.OrdinalIgnoreCase))
		{
			throw new ArgumentException("Unsupported image format.");
		}

		var imageId = Guid.NewGuid();

		var directory = Path.Combine(
			_environment.WebRootPath,
			StaticFilesSettings.AlbumPath
		);

		Directory.CreateDirectory(directory);

		var fileName = $"{imageId}{extension}";

		var filePath = Path.Combine(directory, fileName);

		await using var stream = new FileStream(
			filePath,
			FileMode.Create
		);

		await image.CopyToAsync(stream);

		return imageId;
	}

	public Task DeleteAlbumImageAsync(Guid imageId)
	{
		var directory = Path.Combine(
			_environment.WebRootPath,
			StaticFilesSettings.AlbumPath
		);

		var files = Directory.GetFiles(
			directory,
			$"{imageId}.*"
		);

		foreach (var file in files)
		{
			File.Delete(file);
		}

		return Task.CompletedTask;
	}

	public string GetAlbumImageUrl(Guid imageId, string extension)
	{
		return $"{StaticFilesSettings.WebAlbumPath}/{imageId}{extension}";
	}
}