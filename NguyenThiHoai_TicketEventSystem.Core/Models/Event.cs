using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace NguyenThiHoai_TicketEventSystem.Core.Models
{
    public class Event
    {
        public int Id { get; set; }
        [Required, StringLength(100)] public string Name { get; set; } = string.Empty;
        [Required] public DateTime Date { get; set; }
        [Required, StringLength(200)] public string Location { get; set; } = string.Empty;
        [StringLength(1000)] public string? Description { get; set; }
        [Required, Range(1, int.MaxValue)] public int TicketLimit { get; set; }
        public string CreatedById { get; set; } = string.Empty;
        [ForeignKey("CreatedById")] public virtual ApplicationUser? CreatedBy { get; set; }
        public virtual ICollection<Registration> Registrations { get; set; } = new List<Registration>();
    }
}