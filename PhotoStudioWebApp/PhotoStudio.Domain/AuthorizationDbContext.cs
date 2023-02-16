using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PhotoStudio.Domain.EntityModels;

namespace PhotoStudio.Domain
{
    public class AuthorizationDbContext : IdentityDbContext<IdentityUser>
    {
        public DbSet<EquipmentItem> EquipmentItems { get; set; }
        public DbSet<Rooms> Rooms { get; set; }        
        public AuthorizationDbContext(DbContextOptions<AuthorizationDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
