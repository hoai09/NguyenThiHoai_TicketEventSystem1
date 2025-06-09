using NguyenThiHoai_TicketEventSystem.Core.Models;
using NguyenThiHoai_TicketEventSystem.UseCases.Interfaces;
namespace NguyenThiHoai_TicketEventSystem.UseCases.EventUseCases
{
    public class CreateEventUseCase
    {
        private readonly IEventRepository _eventRepository;
        public CreateEventUseCase(IEventRepository eventRepository) => _eventRepository = eventRepository;
        public async Task ExecuteAsync(Event newEvent) => await _eventRepository.AddAsync(newEvent);
    }
}