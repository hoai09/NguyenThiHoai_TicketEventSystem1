using Microsoft.EntityFrameworkCore;
using NguyenThiHoai_TicketEventSystem.Core.Models;
using NguyenThiHoai_TicketEventSystem.Data.Datas;
using NguyenThiHoai_TicketEventSystem.UseCases.Interfaces;
namespace NguyenThiHoai_TicketEventSystem.Data.Repositories
{
    public class EventRepository : IEventRepository
    {
        private readonly AppDbContext _context;
        public EventRepository(AppDbContext context) { _context = context; }
        public async Task<List<Event>> GetAllAsync() => await _context.Events.Include(e => e.Registrations).OrderByDescending(e => e.Date).ToListAsync();
        public async Task<Event?> GetByIdAsync(int id) => await _context.Events.Include(e => e.CreatedBy).Include(e => e.Registrations).ThenInclude(r => r.User).FirstOrDefaultAsync(e => e.Id == id);
        public async Task AddAsync(Event entity) { await _context.Events.AddAsync(entity); await _context.SaveChangesAsync(); }
        public async Task UpdateAsync(Event entity)
        {
            var existingEvent = await _context.Events.FindAsync(entity.Id);
            if (existingEvent != null)
            {
                _context.Entry(existingEvent).CurrentValues.SetValues(entity);
                await _context.SaveChangesAsync();
            }
        }
        public async Task DeleteAsync(int id)
        {
            var entity = await _context.Events.FindAsync(id);
            if (entity != null) { _context.Events.Remove(entity); await _context.SaveChangesAsync(); }
        }
    }
}