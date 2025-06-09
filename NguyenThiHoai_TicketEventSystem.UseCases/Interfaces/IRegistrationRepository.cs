using NguyenThiHoai_TicketEventSystem.Core.Models;
namespace NguyenThiHoai_TicketEventSystem.UseCases.Interfaces
{
    public interface IRegistrationRepository
    {
        Task AddAsync(Registration registration);
        Task DeleteAsync(int eventId, string userId);
        Task<bool> IsUserRegisteredAsync(int eventId, string userId);
        Task<List<Registration>> GetByEventIdAsync(int eventId);
    }
}