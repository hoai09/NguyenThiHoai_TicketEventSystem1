using NguyenThiHoai_TicketEventSystem.Core.Models;
using NguyenThiHoai_TicketEventSystem.UseCases.Interfaces;
namespace NguyenThiHoai_TicketEventSystem.UseCases.EventUseCases
{
    public class UpdateEventUseCase
    {
        private readonly IEventRepository _eventRepository;
        public UpdateEventUseCase(IEventRepository eventRepository) => _eventRepository = eventRepository;
        public async Task ExecuteAsync(Event eventToUpdate) => await _eventRepository.UpdateAsync(eventToUpdate);
    }
}