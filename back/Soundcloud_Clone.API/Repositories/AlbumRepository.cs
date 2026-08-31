using Microsoft.EntityFrameworkCore;
using Soundcloud_Clone.API.Models;
using Soundcloud_Clone.DAL;
using Soundcloud_Clone.DAL.Enitites;

namespace Soundcloud_Clone.API.Repositories;

public class AlbumRepository : IAlbumRepository
{
	private readonly AppDbContext _context;

	public AlbumRepository(AppDbContext context)
	{
		_context = context;
	}

	public async Task<IEnumerable<Album>> GetAllAsync()
	{
		return await _context.Albums
			.Select(a => new Album
			{
				Id = a.Id,
				Name = a.Name,
				Description = a.Description,
				AuthorId = a.AuthorId
			})
			.ToListAsync();
	}

	public async Task<Album?> GetByIdAsync(int id)
	{
		return await _context.Albums
			.Where(a => a.Id == id)
			.Select(a => new Album
			{
				Id = a.Id,
				Name = a.Name,
				Description = a.Description,
				AuthorId = a.AuthorId
			})
			.FirstOrDefaultAsync();
	}

	public async Task<Album> CreateAsync(Album album)
	{
		var entity = new AlbumEntity
		{
			Name = album.Name,
			Description = album.Description,
			AuthorId = album.AuthorId
		};

		_context.Albums.Add(entity);

		await _context.SaveChangesAsync();

		album.Id = entity.Id;

		return album;
	}

	public async Task<bool> UpdateAsync(Album album)
	{
		var entity = await _context.Albums
			.FirstOrDefaultAsync(a => a.Id == album.Id);

		if (entity is null)
			return false;

		entity.Name = album.Name;
		entity.Description = album.Description;
		entity.AuthorId = album.AuthorId;

		await _context.SaveChangesAsync();

		return true;
	}

	public async Task<bool> DeleteAsync(int id)
	{
		var entity = await _context.Albums
			.FirstOrDefaultAsync(a => a.Id == id);

		if (entity is null)
			return false;

		_context.Albums.Remove(entity);

		await _context.SaveChangesAsync();

		return true;
	}
}