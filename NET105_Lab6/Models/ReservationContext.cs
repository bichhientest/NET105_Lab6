using Microsoft.EntityFrameworkCore;
namespace NET105_Lab6.Models
{
    public class ReservationContext : DbContext
    {
        public ReservationContext(DbContextOptions<ReservationContext> options) : base(options) { }
        public DbSet<Reservation> Reservations { get; set;}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Reservation>(entity =>

            {
                entity.Property(e => e.Name).HasMaxLength(250).IsUnicode(false);
                entity.Property(e => e.StartLocation).HasMaxLength(250).IsUnicode(false);
                entity.Property(e => e.EndLocation).HasMaxLength(250).IsUnicode(false);
            }
            ); 
            
        }
    }
}
