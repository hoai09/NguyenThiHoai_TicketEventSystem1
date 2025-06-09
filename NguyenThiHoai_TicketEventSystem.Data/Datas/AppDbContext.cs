using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NguyenThiHoai_TicketEventSystem.Core.Models;

namespace NguyenThiHoai_TicketEventSystem.Data.Datas
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Event> Events { get; set; }
        public DbSet<Registration> Registrations { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Registration>()
                .HasIndex(r => new { r.EventId, r.UserId })
                .IsUnique();

            modelBuilder.Entity<Event>()
                .HasOne(e => e.CreatedBy)
                .WithMany(u => u.CreatedEvents)
                .HasForeignKey(e => e.CreatedById)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}