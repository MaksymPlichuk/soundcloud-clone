using Microsoft.EntityFrameworkCore;
using Soundcloud_Clone.DAL.Enitites;
using System;
using System.Collections.Generic;
using System.Text;

namespace Soundcloud_Clone.DAL.Repositories
{
    public class SongRepository:GenericRepository<SongEntity>
    {
        private readonly AppDbContext _context;
        public SongRepository(AppDbContext context):base(context)
        {
            _context = context;
        }
        public override IQueryable<SongEntity> GetAll()
        {
            return base.GetAll()
                .Include(s => s.Artist)
                .Include(s => s.Albums)
                    .ThenInclude(a => a.Author)
                .Include(s => s.Comments)
                    .ThenInclude(c => c.Author);
        }

        public override async Task<SongEntity?> GetByIdAsync(int id)
        {
            return await _context.Set<SongEntity>()
                .Include(s => s.Artist)
                .Include(s => s.Albums)
                    .ThenInclude(a => a.Author)
                .Include(s => s.Comments)
                    .ThenInclude(c => c.Author)
                .FirstOrDefaultAsync(s => s.Id == id);
        }
    }
}
