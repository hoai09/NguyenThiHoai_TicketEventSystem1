using NguyenThiHoai_TicketEventSystem.Core.Models;
using NguyenThiHoai_TicketEventSystem.UseCases.Interfaces;
namespace NguyenThiHoai_TicketEventSystem.UseCases.EventUseCases
{
    public class GetEventByIdUseCase
    {
        private readonly IEventRepository _eventRepository;
        public GetEventByIdUseCase(IEventRepository eventRepository) => _eventRepository = eventRepository;
        public async Task<Event?> ExecuteAsync(int eventId) => await _eventRepository.GetByIdAsync(eventId);
    }
}