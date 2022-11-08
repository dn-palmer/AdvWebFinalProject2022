using DnDAPI.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Numerics;

namespace DnDAPI.Services;

//Used to excute CRUD operations against the DB.
public class DndDbRepository : IDnDRepository
{

    private readonly ApplicationDbContext _db;

    public DndDbRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    //*****************************************************************
    //**    Dungeon Master CRUD Opps happen beyond this point.       **
    //*****************************************************************
    public async Task<DungeonMaster> CreateDungeonMasterAsync(DungeonMaster dungeonMaster)
    {
        _db.DungeonMasters.Add(dungeonMaster);
        await _db.SaveChangesAsync();
        return dungeonMaster;
        
    }

    public async Task<ICollection<DungeonMaster>> ReadAllDMAsync()
    {
       return await _db.DungeonMasters
            .Include(c => c.Campaigns)
            .ThenInclude(p => p.Player)
            .ToListAsync();
    }

    public async Task<DungeonMaster> ReadDMAsync(int id)
    {
        return await _db.DungeonMasters
            .Include(c => c.Campaigns)
                .ThenInclude(p => p.Player)
            .FirstOrDefaultAsync(d => d.Id == id);
        
    }

    public async Task UpdateGMAsync(DungeonMaster dungeonMaster)
    {
        var dMToUpdate = await ReadDMAsync(dungeonMaster.Id);

        if (dMToUpdate != null)
        {
            dMToUpdate.Id = dungeonMaster.Id;
            dMToUpdate.FirstName = dungeonMaster.FirstName;
            dMToUpdate.LastName = dungeonMaster.LastName;
            dMToUpdate.YearsOfExperiance = dungeonMaster.YearsOfExperiance;
            dMToUpdate.Email = dungeonMaster.Email;
            dMToUpdate.DungeonAndDragons1E = dungeonMaster.DungeonAndDragons1E;
            dMToUpdate.AdvancedDnD1E = dungeonMaster.AdvancedDnD1E;
            dMToUpdate.AdvancedDnD2E = dungeonMaster.AdvancedDnD2E;
            dMToUpdate.DungeonAndDragons3E = dungeonMaster.DungeonAndDragons3E;
            dMToUpdate.DungeonAndDragons4E = dungeonMaster.DungeonAndDragons4E;
            dMToUpdate.DungeonAndDragons5E = dungeonMaster.DungeonAndDragons5E;
            await _db.SaveChangesAsync();
        }
    }

    //This will change after I finish the many to many aspects
    public Task DeleteGMAsync(int id)
    {
        throw new NotImplementedException();
    }

    //*****************************************************************
    //**    Player CRUD Opps happen beyond this point.               **
    //*****************************************************************
    

    public async Task<Player> CreatePlayerAsync(Player player)
    {
        _db.Players.Add(player);
        await _db.SaveChangesAsync();
        return player;
    }

    public async Task<ICollection<Player>> ReadAllPlayersAsync()
    {
       return await _db.Players.Include(c => c.Campaigns)
            .ThenInclude(d => d.DungeonMaster)
            .ToListAsync();
    }

    public async Task<Player> ReadPlayerAsync(int id)
    {
        return await _db.Players.Include(c => c.Campaigns)
                .ThenInclude(d => d.DungeonMaster)
            .FirstOrDefaultAsync(p => p.Id == id);


    }


    public async Task UpdatePlayerAsync(Player player)
    {
        var playerToUpdate = await ReadPlayerAsync(player.Id);

        if(playerToUpdate != null)
        {
            playerToUpdate.Id = player.Id;
            playerToUpdate.FirstName = player.FirstName;
            playerToUpdate.LastName = player.LastName;
            playerToUpdate.YearsOfExperiance = player.YearsOfExperiance;
            playerToUpdate.Email = player.Email;
            playerToUpdate.DungeonAndDragons1E = player.DungeonAndDragons1E;
            playerToUpdate.AdvancedDnD1E = player.AdvancedDnD1E;
            playerToUpdate.AdvancedDnD2E = player.AdvancedDnD2E;
            playerToUpdate.DungeonAndDragons3E = player.DungeonAndDragons3E;
            playerToUpdate.DungeonAndDragons4E = player.DungeonAndDragons4E;
            playerToUpdate.DungeonAndDragons5E = player.DungeonAndDragons5E;
            await _db.SaveChangesAsync();
        }
    }

    //This will change after I finish the many to many aspects
    public async Task DeletePlayerAsync(int id)
    {
        var player = await ReadPlayerAsync(id);
       
        if(player != null)
        {  
            _db.Players.Remove(player);
            await _db.SaveChangesAsync();
        }

    }

    //*****************************************************************
    //**    Game Preference CRUD Opps happen beyond this point.      **
    //*****************************************************************

    public async Task<Campaign> CreatePreferencesAsync(Campaign campaign)
    {
        _db.Campaigns.Add(campaign);
        await _db.SaveChangesAsync();
        return campaign;
    }
    public async Task<Campaign> ReadCampaignAsync(int id)
    {
        return await _db.Campaigns
            .Include(p => p.Player)
            .Include(d => d.DungeonMaster)
            .FirstOrDefaultAsync(c => c.Id == id);
    }
    public async Task<ICollection<Campaign>> ReadAllCampaignsAsync()
    {
        return await _db.Campaigns
            .Include(p => p.Player)
            .Include(d => d.DungeonMaster)
            .ToListAsync();

    }
    public async Task UpdatePrefrencesAsync(Campaign campaign)
    {

    }

    public Task<Campaign> CreateCampaignAsync(int dungeonMasterId, int playerId)
    {
        throw new NotImplementedException();
    }
}
