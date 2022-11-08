using DnDAPI.Models.Entities;

namespace DnDAPI.Services;
//Interface for the Db. CRUD Operations for the Database of the DB
public interface IDnDRepository
{
    //*****************************************************************
    //**    Dungeon Master CRUD Opps happen beyond this point.       **
    //*****************************************************************
    Task<ICollection<DungeonMaster>> ReadAllDMAsync();
    Task<DungeonMaster> ReadDMAsync(int id);
    Task<DungeonMaster> CreateDungeonMasterAsync(DungeonMaster dungeonMaster);
    Task UpdateGMAsync(DungeonMaster dungeonMaster);
    Task DeleteGMAsync(int id);

    //*****************************************************************
    //**    Player CRUD Opps happen beyond this point.               **
    //*****************************************************************


    Task<ICollection<Player>> ReadAllPlayersAsync();
    Task<Player> ReadPlayerAsync(int id);
    Task<Player> CreatePlayerAsync(Player player);
    Task UpdatePlayerAsync(Player palyer);
    Task DeletePlayerAsync(int id);

    //*****************************************************************
    //**    Game Preference CRUD Opps happen beyond this point.      **
    //*****************************************************************

    public Task<Campaign> CreatePreferencesAsync(Campaign campaign);
    public Task<Campaign> ReadCampaignAsync(int id);
    public Task<ICollection<Campaign>> ReadAllCampaignsAsync();
    Task<Campaign> CreateCampaignAsync(int dungeonMasterId, int playerId);
    public Task UpdatePrefrencesAsync(Campaign campaign);
}
