using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace NguyenThiHoai_TicketEventSystem.Data.Datas
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            
            optionsBuilder.UseSqlServer("Server=DESKTOP-M7C4J1Q;Database=NguyenThiHoai_TicketEventDb;Trusted_Connection=True;MultipleActiveResultSets=true;Encrypt=False");
            return new AppDbContext(optionsBuilder.Options);
        }
    }
}