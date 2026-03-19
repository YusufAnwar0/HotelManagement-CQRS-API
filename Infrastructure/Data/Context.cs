using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class Context: DbContext
    {
        public Context(DbContextOptions<Context> options): base(options) { }


        public DbSet<Facility> Facilities { get; set; }
        public DbSet<Offer> Offers { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<RoomType> RoomTypes { get; set; }
        public DbSet<RoomTypeFacility> RoomTypeFacilities { get; set; }
        public DbSet<RoomTypeOffer> RoomTypeOffer { get; set; }
        public DbSet<RoomReservation> RoomReservations { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(Context).Assembly);
        }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder
                .Properties<decimal>()
                .HavePrecision(18, 2);
        }

    }
}