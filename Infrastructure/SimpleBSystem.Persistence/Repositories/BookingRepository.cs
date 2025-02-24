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
    public class BookingRepository : IBookingRepository
    {
        private readonly AppDbContext _context;

        public BookingRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Booking> GetByIdAsync(int id)
        {
            return await _context.Bookings
                .Include(b => b.Resource)
                .FirstOrDefaultAsync(b => b.Id == id);
        }
        public async Task<IEnumerable<Booking>> GetAllAsync()
        {
            return await _context.Bookings
                .Include(b => b.Resource)
                .ToListAsync();
        }
        public async Task AddAsync(Booking booking)
        {
            await _context.Bookings.AddAsync(booking);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(Booking booking)
        {
            _context.Bookings.Update(booking);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(Booking booking)
        {
            _context.Bookings.Remove(booking);
            await _context.SaveChangesAsync();
        }
        public async Task<IEnumerable<Booking>> GetBookingsByResourceIdAsync(int resourceId)
        {
            return await _context.Bookings
                .Include(b => b.Resource)
                .Where(b => b.ResourceId == resourceId)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
