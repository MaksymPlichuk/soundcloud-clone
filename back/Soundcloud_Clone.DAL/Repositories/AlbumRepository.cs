using Microsoft.EntityFrameworkCore;
using Soundcloud_Clone.DAL;
using Soundcloud_Clone.DAL.Enitites;
using Soundcloud_Clone.DAL.Repositories;

namespace Soundcloud_Clone.API.Repositories;

public class AlbumRepository : GenericRepository<AlbumEntity>
{
	private readonly AppDbContext _context;
	public AlbumRepository(AppDbContext context) : base(context)
	{
		_context = context;
	}
    public override IQueryable<AlbumEntity> GetAll()
    {
        return base.GetAll()
            .Include(a => a.Author)
            .Include(a => a.Songs)
                .ThenInclude(s => s.Artist);
    }

    public override async Task<AlbumEntity?> GetByIdAsync(int id)
    {
        return await _context.Set<AlbumEntity>()
            .Include(a => a.Author)
            .Include(a => a.Songs)
                .ThenInclude(s => s.Artist)
            .FirstOrDefaultAsync(a => a.Id == id);
    }
}