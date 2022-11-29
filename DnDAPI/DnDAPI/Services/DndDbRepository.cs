using DnDAPI.Models.Entities;
using Microsoft.EntityFrameworkCore;
using NuGet.Versioning;
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

    public async Task<DungeonMaster?> ReadDMAsync(int id)
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
    public async Task DeleteGMAsync(int id)
    {
        var dungeonMaster = await ReadDMAsync(id);

        if (dungeonMaster != null)
        {
            _db.DungeonMasters.Remove(dungeonMaster);
            await _db.SaveChangesAsync();
        }
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

    public async Task<Player?> ReadPlayerAsync(int id)
    {
        return await _db.Players.Include(c => c.Campaigns)
                .ThenInclude(d => d.DungeonMaster)
            .FirstOrDefaultAsync(p => p.Id == id);


    }


    public async Task UpdatePlayerAsync(Player player)
    {
        var playerToUpdate = await ReadPlayerAsync(player.Id);

        if (playerToUpdate != null)
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

        if (player != null)
        {
            _db.Players.Remove(player);
            await _db.SaveChangesAsync();
        }

    }

    //*****************************************************************
    //**    Game Preference CRUD Opps happen beyond this point.      **
    //*****************************************************************

    public async Task<Campaign?> ReadCampaignAsync(int id)
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
    public async Task UpdateCampaignAsync(Campaign campaign)
    {
        var dungeonMaster = ReadDMAsync(campaign.DungeonMasterId);
        //if player and dm are not found it terminates the method call.
        if (dungeonMaster == null)
        {
            return;
        }
        var player = await ReadPlayerAsync(campaign.PlayerId);

        if (player == null)
        {
            return;
        }

        var campaignToUpdate = await ReadCampaignAsync(campaign.Id);

        campaignToUpdate.GameEdition = campaign.GameEdition;
        campaignToUpdate.CampaignName = campaign.CampaignName;
        campaignToUpdate.CampaignDescription = campaign.CampaignDescription;
        campaignToUpdate.DungeonMasterId = campaign.DungeonMasterId;
        campaignToUpdate.DungeonMaster = campaign.DungeonMaster;
        campaignToUpdate.Player = campaign.Player;
        campaignToUpdate.PlayerId = campaign.PlayerId;
        await _db.SaveChangesAsync();
    }

    public async Task<Campaign?> CreateCampaignAsync(Campaign campaign)
    {
        var dungeonMaster = await ReadDMAsync(campaign.DungeonMasterId);

        if (dungeonMaster == null)
        {
            return null;
        }

        var player = await ReadPlayerAsync(campaign.PlayerId);

        if (player == null)
        {
            return null;
        }
        dungeonMaster.Campaigns.Add(campaign);
        player.Campaigns.Add(campaign);
        await _db.SaveChangesAsync();
        return campaign;

        //large chance I am wasting my time with this but just in case it has been created.
        //var newCampaign = new Campaign
        //{
        //    GameEdition = campaign.GameEdition,
        //    CampaignName = campaign.CampaignName,
        //    CampaignDescription = campaign.CampaignDescription,
        //    DungeonMasterId = dungeonMaster.Id,
        //    DungeonMaster = dungeonMaster,
        //    Player = player,
        //    PlayerId = player.Id,
        //};
        //dungeonMaster.Campaigns.Add(newCampaign);
        //player.Campaigns.Add(newCampaign);

    }

    public async Task RemoveCampaignFromPlayerAsync(int playerId, int campainId)
    {
        var player = await ReadPlayerAsync(playerId);
        var campain = player!.Campaigns.FirstOrDefault(c => c.Id == campainId);
        var dungeonMaster = campain!.DungeonMaster;
        player!.Campaigns.Remove(campain);
        dungeonMaster!.Campaigns.Remove(campain);
        await _db.SaveChangesAsync();
        
    }

    public async Task RemoveCampaignFromDungeonMasterAsync(int dungeonMasterId, int campainId)
    {
        var dungeonMaster = await ReadDMAsync(dungeonMasterId);
        var allCampains = await ReadAllCampaignsAsync();
        var campain = await ReadCampaignAsync(campainId);

        //removes all players from the campain before we remove the DM(because it cannot exist without 1 of them.) Need to triple check that this works. 
        foreach (var c in allCampains)
        {
            if(c.CampaignName == campain.CampaignName )
            {
                var player = await ReadPlayerAsync(c.PlayerId);
                player!.Campaigns.Remove(c);
                dungeonMaster!.Campaigns.Remove(c);
                await _db.SaveChangesAsync();
            }                       
        }

    }
}
