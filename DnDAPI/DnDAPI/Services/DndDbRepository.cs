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
       return await _db.DungeonMasters.Include(p => p.Players).ToListAsync();
    }

    public async Task<DungeonMaster> ReadDMAsync(int id)
    {
        var dungeonMaster = await _db.DungeonMasters.FindAsync(id);

        if(dungeonMaster != null)
        {
            dungeonMaster.GamePreferences = await ReadPreferencesAsync(id);
            _db.Entry(dungeonMaster).Collection(p => p.Players).Load();                
        }

        return dungeonMaster;
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
            dMToUpdate.GamePreferences = dungeonMaster.GamePreferences;
            dMToUpdate.Players = dungeonMaster.Players;
            await _db.SaveChangesAsync();
        }
    }

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
       return await _db.Players.ToListAsync();
    }

    public async Task<Player> ReadPlayerAsync(int id)
    {
      var player = await _db.Players.FindAsync(id);
        player.GamePreferences = await ReadPreferencesAsync(id);
      return player;

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
            playerToUpdate.GamePreferences = player.GamePreferences;
            playerToUpdate.DungeonMasterId = player.DungeonMasterId;
            playerToUpdate.DungeonMaster = player.DungeonMaster;        
            await _db.SaveChangesAsync();
        }
    }

    public async Task DeletePlayerAsync(int id)
    {
        var player = await ReadPlayerAsync(id);
       
        if(player != null)
        {
            var gamePreference = await _db.GamePreferences.FirstOrDefaultAsync(p => p.UserId == player.Id);
            _db.GamePreferences.Remove(gamePreference);
            _db.Players.Remove(player);
            await _db.SaveChangesAsync();
        }

    }

    //*****************************************************************
    //**    Game Preference CRUD Opps happen beyond this point.      **
    //*****************************************************************

    public async Task<GamePreferences> CreatePreferencesAsync(GamePreferences gamePreferences)
    {
        _db.GamePreferences.Add(gamePreferences);
        await _db.SaveChangesAsync();
        return gamePreferences;
    }
    public async Task<GamePreferences> ReadPreferencesAsync(int userId)
    {
        return await _db.GamePreferences.FirstOrDefaultAsync(u => u.UserId == userId);
    }

    public async Task UpdatePrefrencesAsync(GamePreferences preferences)
    {
        var preferenceToUpdate = await ReadPreferencesAsync(preferences.Id);

        if (preferenceToUpdate != null)
        {
            preferenceToUpdate.Id = preferences.Id;
            preferenceToUpdate.UserId = preferences.UserId;
            preferenceToUpdate.DungeonAndDragons1E = preferences.DungeonAndDragons1E;
            preferenceToUpdate.AdvancedDnD1E = preferences.AdvancedDnD1E;
            preferenceToUpdate.AdvancedDnD2E = preferences.AdvancedDnD2E;
            preferenceToUpdate.DungeonAndDragons3E = preferences.DungeonAndDragons3E;
            preferenceToUpdate.DungeonAndDragons4E = preferences.DungeonAndDragons4E;
            preferenceToUpdate.DungeonAndDragons5E = preferences.DungeonAndDragons5E;
            await _db.SaveChangesAsync();
        }
    }

}
