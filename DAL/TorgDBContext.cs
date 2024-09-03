using DAL.Configs;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace DAL
{
    public class TorgDBContext : DbContext, ITorgDBContext
    {
        public TorgDBContext(DbContextOptions<TorgDBContext> options) : base(options) 
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CustomerConfig());
            modelBuilder.ApplyConfiguration(new PhotoConfig());
            modelBuilder.ApplyConfiguration(new RoleConfig());
            modelBuilder.ApplyConfiguration(new TradingPointConfig());
            modelBuilder.ApplyConfiguration(new TripConfig());
            modelBuilder.ApplyConfiguration(new UserConfig());
            modelBuilder.ApplyConfiguration(new VisitConfig());
            modelBuilder.ApplyConfiguration(new WorkRegionConfig());
        }

        public virtual DbSet<Customer> Customer { get; set; }
        public virtual DbSet<Photo> Photo { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<TradingPoint> TradingPoint { get; set; }
        public virtual DbSet<Trip> Trip { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<Visit> Visit { get; set; }
        public virtual DbSet<WorkRegion> WorkRegion { get; set; }

    }
}
