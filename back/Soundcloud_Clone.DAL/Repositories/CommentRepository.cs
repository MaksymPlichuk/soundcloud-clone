using Microsoft.EntityFrameworkCore;
using Soundcloud_Clone.DAL.Enitites;
using Soundcloud_Clone.DAL.Enitites.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Soundcloud_Clone.DAL.Repositories
{
    public class CommentRepository : GenericRepository<CommentEntity>
    {
        private readonly AppDbContext _context;
        public CommentRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
