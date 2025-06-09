using NguyenThiHoai_TicketEventSystem.Core.Models;
namespace NguyenThiHoai_TicketEventSystem.UseCases.Interfaces
{
    public interface IEventRepository
    {
        Task<Event?> GetByIdAsync(int id);
        Task<List<Event>> GetAllAsync();
        Task AddAsync(Event entity);
        Task UpdateAsync(Event entity);
        Task DeleteAsync(int id);
    }
}