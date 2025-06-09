using Microsoft.EntityFrameworkCore;
using NguyenThiHoai_TicketEventSystem.Core.Models;
using NguyenThiHoai_TicketEventSystem.Data.Datas;
using NguyenThiHoai_TicketEventSystem.UseCases.Interfaces;
namespace NguyenThiHoai_TicketEventSystem.Data.Repositories
{
    public class RegistrationRepository : IRegistrationRepository
    {
        private readonly AppDbContext _context;
        public RegistrationRepository(AppDbContext context) { _context = context; }
        public async Task AddAsync(Registration registration) { await _context.Registrations.AddAsync(registration); await _context.SaveChangesAsync(); }
        public async Task<bool> IsUserRegisteredAsync(int eventId, string userId) => await _context.Registrations.AnyAsync(r => r.EventId == eventId && r.UserId == userId);
        public async Task<List<Registration>> GetByEventIdAsync(int eventId) => await _context.Registrations.Where(r => r.EventId == eventId).Include(r => r.User).ToListAsync();
        public async Task DeleteAsync(int eventId, string userId)
        {
            var entity = await _context.Registrations.FirstOrDefaultAsync(r => r.EventId == eventId && r.UserId == userId);
            if (entity != null) { _context.Registrations.Remove(entity); await _context.SaveChangesAsync(); }
        }
    }
}