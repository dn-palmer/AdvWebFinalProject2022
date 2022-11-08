using DnDAPI.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace DnDAPI.Services;
//The Database Context class that will be used to interact with and form the database and migration. 
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {

    }

    public DbSet<DungeonMaster> DungeonMasters => Set<DungeonMaster>();
    public DbSet<Player> Players => Set<Player>();
    public DbSet<Campaign> Campaigns => Set<Campaign>();

}
