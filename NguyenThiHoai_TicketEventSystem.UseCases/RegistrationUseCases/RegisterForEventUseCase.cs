using NguyenThiHoai_TicketEventSystem.Core.Models;
using NguyenThiHoai_TicketEventSystem.UseCases.Interfaces;
namespace NguyenThiHoai_TicketEventSystem.UseCases.RegistrationUseCases
{
    public class RegisterForEventUseCase
    {
        private readonly IRegistrationRepository _regRepo; private readonly IEventRepository _eventRepo;
        public RegisterForEventUseCase(IRegistrationRepository regRepo, IEventRepository eventRepo) { _regRepo = regRepo; _eventRepo = eventRepo; }
        public async Task<(bool Success, string Message)> ExecuteAsync(int eventId, string userId)
        {
            if (await _regRepo.IsUserRegisteredAsync(eventId, userId)) return (false, "Bạn đã đăng ký sự kiện này rồi.");
            var ev = await _eventRepo.GetByIdAsync(eventId);
            if (ev == null) return (false, "Sự kiện không tồn tại.");
            if (ev.Registrations.Count >= ev.TicketLimit) return (false, "Sự kiện đã hết vé.");
            await _regRepo.AddAsync(new Registration { EventId = eventId, UserId = userId, RegistrationDate = DateTime.UtcNow });
            return (true, "Đăng ký thành công!");
        }
    }
}