using Microsoft.EntityFrameworkCore;
using SimpleBSystem.Application.Interfaces;
using SimpleBSystem.Domain.Entities;
using SimpleBSystem.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBSystem.Persistence.Repositories
{
    public class ResourceRepository : IResourceRepository
    {
        private readonly AppDbContext _context;

        public ResourceRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Resource resource)
        {
            await _context.Resources.AddAsync(resource);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Resource resource)
        {
            _context.Resources.Remove(resource);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Resource>> GetAllAsync()
        {
            return await _context.Resources.AsNoTracking().ToListAsync();
        }

        public async Task<Resource> GetByIdAsync(int id)
        {
            return await _context.Resources
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task UpdateAsync(Resource resource)
        {
            _context.Resources.Update(resource);
            await _context.SaveChangesAsync();
        }
    }
}
