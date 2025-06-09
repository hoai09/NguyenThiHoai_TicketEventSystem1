using NguyenThiHoai_TicketEventSystem.Core.Models;
using NguyenThiHoai_TicketEventSystem.UseCases.Interfaces;
namespace NguyenThiHoai_TicketEventSystem.UseCases.EventUseCases
{
    public class GetAllEventsUseCase
    {
        private readonly IEventRepository _eventRepository;
        public GetAllEventsUseCase(IEventRepository eventRepository) => _eventRepository = eventRepository;
        public async Task<List<Event>> ExecuteAsync() => await _eventRepository.GetAllAsync();
    }
}