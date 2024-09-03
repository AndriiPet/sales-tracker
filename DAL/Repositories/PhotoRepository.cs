using DAL.Entities;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class PhotoRepository : IPhotoRepository
    {

        private readonly TorgDBContext _context;

        public PhotoRepository(TorgDBContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Photo entity)
        {
            await _context.Photo.AddAsync(entity);
        }

        public async Task DeleteByIdAsync(int id)
        {
            var photo = await _context.Photo.FindAsync(id);
            if (photo != null)
                _context.Photo.Remove(photo);
        }

        public async Task<IEnumerable<Photo>> GetAllAsync()
        {
            return await _context.Photo.ToListAsync();
        }

        public async Task<Photo> GetByIdAsync(int id)
        {
            return await _context.Photo.FindAsync(id);
        }

        public async Task<IEnumerable<Photo>> GetByVisitIdAsync(int visitId)
        {
            return await _context.Photo
            .Include(p => p.Visit)
            .Where(p => p.VisitId == visitId)
            .ToListAsync();
        }

        public void Update(Photo entity)
        {
            _context.Photo.Update(entity);
        }
    }
}
