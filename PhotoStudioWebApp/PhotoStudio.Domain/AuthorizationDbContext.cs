using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PhotoStudio.Domain.EntityModels;
using PhotoStudio.Domain.Helper;

namespace PhotoStudio.Domain
{
    public class AuthorizationDbContext : IdentityDbContext<IdentityUser>
    {
        

        public DbSet<EquipmentItem> EquipmentItems { get; set; }
        public DbSet<Room> Rooms { get; set; }        
        public DbSet<RoomBooking> RoomBookings { get; set; }
        public AuthorizationDbContext(DbContextOptions<AuthorizationDbContext> options) : base(options)
        {
            
        }
        public AuthorizationDbContext()
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
#if DEBUG
            optionsBuilder.EnableSensitiveDataLogging();
#endif
            base.OnConfiguring(optionsBuilder);
        }

        private void SeedAdmin(ModelBuilder builder)
        {
            // any guid
            const string ADMIN_ID = "a18be9c0-aa65-4af8-bd17-00bd9344e575";
            // any guid, but nothing is against to use the same one
            const string ROLE_ID = "725453E0-3BD1-4CDA-B6A4-F4A683B987ED";
            builder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Id = ROLE_ID,
                Name = PhotoStudio.Core.UserRoles.Admin,
                NormalizedName = PhotoStudio.Core.UserRoles.Admin
            });

            var hasher = new PasswordHasher<IdentityUser>();
            builder.Entity<IdentityUser>().HasData(new IdentityUser
            {
                Id = ADMIN_ID,
                UserName = "admin",
                NormalizedUserName = "admin",
                Email = "admin@admin.com",
                NormalizedEmail = "admin@admin.com",
                EmailConfirmed = true,
                PasswordHash = hasher.HashPassword(null, "adminpassword"),
                SecurityStamp = string.Empty
            });

            builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = ROLE_ID,
                UserId = ADMIN_ID
            });
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            SeedAdmin(builder);
            base.OnModelCreating(builder);
        }
    }
}
