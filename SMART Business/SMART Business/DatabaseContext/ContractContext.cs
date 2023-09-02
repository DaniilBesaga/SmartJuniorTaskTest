using Microsoft.EntityFrameworkCore;
using SMART_Business.Models;

namespace SMART_Business.DatabaseContext
{
    public class ContractContext : DbContext
    {
        public ContractContext() { }
        public ContractContext(DbContextOptions<ContractContext> options):base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Contract>()
                .HasOne(o => o.Room)
                .WithMany(o => o.Contracts)
                .HasForeignKey(o => o.RoomId);
            modelBuilder.Entity<Contract>()
               .HasOne(o=>o.Type)
               .WithMany(o => o.Contracts)
               .HasForeignKey(o => o.TypeId);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=testsmart.database.windows.net; Initial Catalog=SMART testdb; Authentication=Active Directory Default; Encrypt=True;");
        }
        public DbSet<Contract> Contracts { get; set; }
        public DbSet<ProductionRoom> Rooms { get; set; }
        public DbSet<TypeOfTE> Types { get; set; }
    }
}
