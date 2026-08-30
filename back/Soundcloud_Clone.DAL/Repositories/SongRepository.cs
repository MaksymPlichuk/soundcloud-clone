using Soundcloud_Clone.DAL.Enitites;
using System;
using System.Collections.Generic;
using System.Text;

namespace Soundcloud_Clone.DAL.Repositories
{
    public class SongRepository:GenericRepository<SongEntity>
    {
        private AppDbContext _context;
        public SongRepository(AppDbContext context):base(context)
        {
            
        }
    }
}
