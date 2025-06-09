using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
namespace NguyenThiHoai_TicketEventSystem.Core.Models
{
    public class ApplicationUser : IdentityUser
    {
        public virtual ICollection<Event> CreatedEvents { get; set; } = new List<Event>();
        public virtual ICollection<Registration> Registrations { get; set; } = new List<Registration>();
    }
}