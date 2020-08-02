using Api.Security.Constraint;
using Api.Security.Database.Model;
using Microsoft.EntityFrameworkCore;

namespace Api.Security.Database
{
    public class DataContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<AccessToken> AccessTokens { get; set; }
        public DbSet<Client> Clients { get; set; }

        public DbSet<UserToken> UserTokens { get; set; }

        public DataContext(DbContextOptions opt) : base(opt)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                new User() { Id = 1, FullName = "Ahmet İhsan Çelik", Username = "ihsancelik", Password = "1", },
                new User() { Id = 2, FullName = "Test User", Username = "test", Password = "1", });

            modelBuilder.Entity<Client>().HasData(
                new Client() { Id = 1, Name = "Windows Desktop", Description = "", Type = ClientTypes.DesktopA },
                new Client() { Id = 2, Name = "MAC Desktop", Description = "", Type = ClientTypes.DesktopB },
                new Client() { Id = 3, Name = "Linux Desktop", Description = "", Type = ClientTypes.DesktopC },
                new Client() { Id = 4, Name = "Android Mobile", Description = "", Type = ClientTypes.MobileA },
                new Client() { Id = 5, Name = "IOS Mobile", Description = "", Type = ClientTypes.MobileB },
                new Client() { Id = 6, Name = "Windows Mobile", Description = "", Type = ClientTypes.MobileC },
                new Client() { Id = 7, Name = "Chrome Browser", Description = "", Type = ClientTypes.BrowserA },
                new Client() { Id = 8, Name = "Edge Browser", Description = "", Type = ClientTypes.BrowserB },
                new Client() { Id = 9, Name = "Firefox Browser", Description = "", Type = ClientTypes.BrowserC });
        }
    }
}
