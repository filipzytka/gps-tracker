using ArduinoServer.Entity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ArduinoServer.Context
{
    public class AppIdentityDbContext : IdentityDbContext<User>
    {
        public AppIdentityDbContext(DbContextOptions<AppIdentityDbContext> options) : base(options) { }

        public DbSet<Device> Devices { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Device>()
                .HasKey(d => d.MacAddress);

            modelBuilder.Entity<Device>().HasData(
                new Device
                {
                    MacAddress = "C4:D8:D5:14:28:D",
                    Name = "ESP8266",
                }
            );
        }
    }
}
