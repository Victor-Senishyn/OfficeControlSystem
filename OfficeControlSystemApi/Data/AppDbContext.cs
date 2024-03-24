using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OfficeControlSystemApi.Models;
using OfficeControlSystemApi.Models.Identity;

namespace OfficeControlSystemApi.Data
{
    public class AppDbContext : IdentityDbContext<User>
    {
        protected readonly IConfiguration Configuration;

        public AppDbContext(IConfiguration configuration, 
            DbContextOptions<AppDbContext> options) : base(options) 
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseNpgsql(Configuration.GetConnectionString("WebApiDatabase"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AccessCard>()
                .HasOne<Employee>()
                .WithMany(e => e.AccessCards)
                .HasForeignKey(ac => ac.EmployeeId);
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<AccessCard> AccessCards { get; set; }
        public DbSet<VisitHistory> VisitHistories { get; set; }
        public DbSet<User> Users {  get; set; }
    }
}
