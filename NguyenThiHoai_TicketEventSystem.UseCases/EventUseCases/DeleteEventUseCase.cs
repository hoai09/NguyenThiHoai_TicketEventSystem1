using NguyenThiHoai_TicketEventSystem.UseCases.Interfaces;
namespace NguyenThiHoai_TicketEventSystem.UseCases.EventUseCases
{
    public class DeleteEventUseCase
    {
        private readonly IEventRepository _eventRepository;
        public DeleteEventUseCase(IEventRepository eventRepository) => _eventRepository = eventRepository;
        public async Task ExecuteAsync(int eventId) => await _eventRepository.DeleteAsync(eventId);
    }
}