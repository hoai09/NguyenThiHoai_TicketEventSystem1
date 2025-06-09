using NguyenThiHoai_TicketEventSystem.UseCases.Interfaces;
namespace NguyenThiHoai_TicketEventSystem.UseCases.RegistrationUseCases
{
    public class UnregisterFromEventUseCase
    {
        private readonly IRegistrationRepository _registrationRepository;
        public UnregisterFromEventUseCase(IRegistrationRepository registrationRepository) => _registrationRepository = registrationRepository;
        public async Task ExecuteAsync(int eventId, string userId) => await _registrationRepository.DeleteAsync(eventId, userId);
    }
}