using System;
using System.ComponentModel.DataAnnotations.Schema;
namespace NguyenThiHoai_TicketEventSystem.Core.Models
{
    public class Registration
    {
        public int Id { get; set; }
        public DateTime RegistrationDate { get; set; }
        public int EventId { get; set; }
        public string UserId { get; set; } = string.Empty;
        [ForeignKey("EventId")] public virtual Event? Event { get; set; }
        [ForeignKey("UserId")] public virtual ApplicationUser? User { get; set; }
    }
}