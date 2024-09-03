using DAL.Entities;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly TorgDBContext _context;

        public RoleRepository(TorgDBContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Role entity)
        {
            await _context.Role.AddAsync(entity);
        }

        public async Task DeleteByIdAsync(int id)
        {
            var role = await _context.Role.FindAsync(id);
            if(role != null)
               await _context.Role.AddAsync(role);
        }

        public async Task<IEnumerable<Role>> GetAllAsync()
        {
            return await _context.Role.ToListAsync();
        }

        public async Task<Role> GetByIdAsync(int id)
        {
            return await _context.Role.FindAsync(id);
        }

        public async Task<Role> GetByNameAsync(string name)
        {
            return await _context.Role
             .FirstOrDefaultAsync(r => r.Name.ToLower() == name.ToLower());
        }

        public void Update(Role entity)
        {
            _context.Role.Update(entity);
        }
    }
}
