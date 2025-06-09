using NguyenThiHoai_TicketEventSystem.Core.Models;
using NguyenThiHoai_TicketEventSystem.UseCases.Interfaces;
namespace NguyenThiHoai_TicketEventSystem.UseCases.RegistrationUseCases
{
    public class GetRegistrationsForEventUseCase
    {
        private readonly IRegistrationRepository _registrationRepository;
        public GetRegistrationsForEventUseCase(IRegistrationRepository registrationRepository) => _registrationRepository = registrationRepository;
        public async Task<List<Registration>> ExecuteAsync(int eventId) => await _registrationRepository.GetByEventIdAsync(eventId);
    }
}