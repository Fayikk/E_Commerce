using E_CommerceForUdemy_Business.Repository.IRepository;
using E_CommerceForUdemy_DataAccess;
using E_CommerceForUdemy_DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceForUdemy_Business.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        public UserRepository(ApplicationDbContext context)
        {
            _context = context; 
        }

        public async Task<ApplicationUser> GetUserByEmail(string email)
        {
            if (email != null)
            {
                var user = await _context.ApplicationUsers.FirstOrDefaultAsync(x=>x.Email == email);  
                if (user != null)
                {
                    return user;
                }
            }
            return null;
        }
    }
}
