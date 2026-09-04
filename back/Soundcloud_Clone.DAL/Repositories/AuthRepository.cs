using Microsoft.EntityFrameworkCore;
using Soundcloud_Clone.DAL.Enitites.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Soundcloud_Clone.DAL.Repositories
{
    public class AuthRepository
    {
        private readonly AppDbContext _context;

        public AuthRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<UserEntity?> CheckUserExists(string email)
        {
            var res = await _context.Users.Where(u => u.Email == email).FirstOrDefaultAsync(); ;
            if (res != null) { return res; }
            else { return null; }
        }
    }
}