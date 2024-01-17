using DominicanPingPong.Models;
using Microsoft.EntityFrameworkCore;

namespace DominicanPingPong.Data
{
    public class DominicanDbContext : DbContext
    {
        public DominicanDbContext(DbContextOptions options) : base(options) {
        
        }

        public DbSet<PlayerModel> Players { get; set; }
    }
}
