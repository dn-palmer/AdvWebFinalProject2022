using DnDShopWebsite.Models.Entities;
using System.Numerics;
using System.Text.Json;

namespace DnDShopWebsite.Services;

public class DnDDbRepository : IDnDRepository
{
    private readonly HttpClient _client;

    public DnDDbRepository(HttpClient client)
    {
        _client = client;
        _client.BaseAddress = new Uri("https://localhost:7108/api/");
    }



    //*****************************************************************
    //**    Dungeon Master CRUD Opps happen beyond this point.       **
    //*****************************************************************
    public async Task<DungeonMaster> CreateDungeonMasterAsync(DungeonMaster dungeonMaster)
    {
        var dungeonMasterData = new FormUrlEncodedContent(new Dictionary<string, string>()
        {
            ["Id"] = $"{dungeonMaster.Id}",
            ["FirstName"] = $"{dungeonMaster.FirstName}",
            ["LastName"] = $"{dungeonMaster.LastName}",
            ["Email"] = $"{dungeonMaster.Email}",
            ["YearsOfExperiance"] = $"{dungeonMaster.YearsOfExperiance}",
            ["DungeonAndDragons1E"] = $"{dungeonMaster.DungeonAndDragons1E}",
            ["AdvancedDnD1E"] = $"{dungeonMaster.AdvancedDnD1E}",
            ["AdvancedDnD2E"] = $"{dungeonMaster.AdvancedDnD2E}",
            ["DungeonAndDragons3E"] = $"{dungeonMaster.DungeonAndDragons3E}",
            ["DungeonAndDragons4E"] = $"{dungeonMaster.DungeonAndDragons4E}",
            ["DungeonAndDragons5E"] = $"{dungeonMaster.DungeonAndDragons5E}"

        });

        var result = await _client.PostAsync("DungeonMaster/Create", dungeonMasterData);

        if(result.IsSuccessStatusCode) 
        {
            return dungeonMaster;
        }

        return null;
    }

    public async Task<ICollection<DungeonMaster>> ReadAllDMAsync()
    {
        List<DungeonMaster>? dungeonMasters = null;

        var response = await _client.GetAsync("DungeonMaster/ReadAll");

        if (response.IsSuccessStatusCode)
        {
            var json = await response.Content.ReadAsStringAsync();

            var serializeOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            dungeonMasters = JsonSerializer.Deserialize<List<DungeonMaster>>(json, serializeOptions);
        }

        dungeonMasters ??= new List<DungeonMaster>();
        return dungeonMasters;

    }

    public async Task<DungeonMaster?> ReadDMAsync(int id)
    {
        DungeonMaster? dungeonMaster = null;

        var response = await _client.GetAsync($"DungeonMaster/Read/{id}");

        if (response.IsSuccessStatusCode)
        {
            var json = await response.Content.ReadAsStringAsync();

            var serializeOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            dungeonMaster = JsonSerializer.Deserialize<DungeonMaster>(json, serializeOptions);
        }
        return dungeonMaster;
    }

    public async Task UpdateGMAsync(DungeonMaster dungeonMaster)
    {
        var dungeonMasterData = new FormUrlEncodedContent(new Dictionary<string, string>()
        {
            ["Id"] = $"{dungeonMaster.Id}",
            ["FirstName"] = $"{dungeonMaster.FirstName}",
            ["LastName"] = $"{dungeonMaster.LastName}",
            ["Email"] = $"{dungeonMaster.Email}",
            ["YearsOfExperiance"] = $"{dungeonMaster.YearsOfExperiance}",
            ["DungeonAndDragons1E"] = $"{dungeonMaster.DungeonAndDragons1E}",
            ["AdvancedDnD1E"] = $"{dungeonMaster.AdvancedDnD1E}",
            ["AdvancedDnD2E"] = $"{dungeonMaster.AdvancedDnD2E}",
            ["DungeonAndDragons3E"] = $"{dungeonMaster.DungeonAndDragons3E}",
            ["DungeonAndDragons4E"] = $"{dungeonMaster.DungeonAndDragons4E}",
            ["DungeonAndDragons5E"] = $"{dungeonMaster.DungeonAndDragons5E}"

        });

         await _client.PutAsync("DungeonMaster/Update", dungeonMasterData);

    }

    
    public async Task DeleteGMAsync(int id)
    {
        await _client.DeleteAsync($"DungeonMaster/Delete/{id}");
    }

    //*****************************************************************
    //**    Player CRUD Opps happen beyond this point.               **
    //*****************************************************************


    public async Task<Player> CreatePlayerAsync(Player player)
    {
        var playerData = new FormUrlEncodedContent(new Dictionary<string, string>()
        {
            ["Id"] = $"{player.Id}",
            ["FirstName"] = $"{player.FirstName}",
            ["LastName"] = $"{player.LastName}",
            ["Email"] = $"{player.Email}",
            ["YearsOfExperiance"] = $"{player.YearsOfExperiance}",
            ["DungeonAndDragons1E"] = $"{player.DungeonAndDragons1E}",
            ["AdvancedDnD1E"] = $"{player.AdvancedDnD1E}",
            ["AdvancedDnD2E"] = $"{player.AdvancedDnD2E}",
            ["DungeonAndDragons3E"] = $"{player.DungeonAndDragons3E}",
            ["DungeonAndDragons4E"] = $"{player.DungeonAndDragons4E}",
            ["DungeonAndDragons5E"] = $"{player.DungeonAndDragons5E}"

        });

        var result = await _client.PostAsync("Player/Create", playerData);

        if (result.IsSuccessStatusCode)
        {
            return player;
        }

        return null;
    }

    public async Task<ICollection<Player>> ReadAllPlayersAsync()
    {
        List<Player>? players = null;

        var response = await _client.GetAsync("Player/ReadAll");

        if (response.IsSuccessStatusCode)
        {
            var json = await response.Content.ReadAsStringAsync();

            var serializeOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            players = JsonSerializer.Deserialize<List<Player>>(json, serializeOptions);
        }

        players ??= new List<Player>();
        return players;

    }

    public async Task<Player?> ReadPlayerAsync(int id)
    {
        Player? player = null;

        var response = await _client.GetAsync($"Player/Read/{id}");

        if (response.IsSuccessStatusCode)
        {
            var json = await response.Content.ReadAsStringAsync();

            var serializeOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            player = JsonSerializer.Deserialize<Player>(json, serializeOptions);
        }

        return player;

    }


    public async Task UpdatePlayerAsync(Player player)
    {
        var playerData = new FormUrlEncodedContent(new Dictionary<string, string>()
        {
            ["Id"] = $"{player.Id}",
            ["FirstName"] = $"{player.FirstName}",
            ["LastName"] = $"{player.LastName}",
            ["Email"] = $"{player.Email}",
            ["YearsOfExperiance"] = $"{player.YearsOfExperiance}",
            ["DungeonAndDragons1E"] = $"{player.DungeonAndDragons1E}",
            ["AdvancedDnD1E"] = $"{player.AdvancedDnD1E}",
            ["AdvancedDnD2E"] = $"{player.AdvancedDnD2E}",
            ["DungeonAndDragons3E"] = $"{player.DungeonAndDragons3E}",
            ["DungeonAndDragons4E"] = $"{player.DungeonAndDragons4E}",
            ["DungeonAndDragons5E"] = $"{player.DungeonAndDragons5E}"

        });

        await _client.PutAsync("Player/Update", playerData);
    }

     public async Task DeletePlayerAsync(int id)
    {
        await _client.DeleteAsync($"Player/Delete/{id}");
    }

    //*****************************************************************
    //**    Game Preference CRUD Opps happen beyond this point.      **
    //*****************************************************************

    public async Task<Campaign?> ReadCampaignAsync(int id)
    {
        Campaign? campaign = null;

        var response = await _client.GetAsync($"Campaign/Read/{id}");

        if (response.IsSuccessStatusCode)
        {
            var json = await response.Content.ReadAsStringAsync();

            var serializeOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            campaign = JsonSerializer.Deserialize<Campaign>(json, serializeOptions);
        }

        return campaign;
    }
    public async Task<ICollection<Campaign>> ReadAllCampaignsAsync()
    {
        List<Campaign>? campaigns = null;

        var response = await _client.GetAsync("Campaign/ReadAll");

        if (response.IsSuccessStatusCode)
        {
            var json = await response.Content.ReadAsStringAsync();

            var serializeOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            campaigns = JsonSerializer.Deserialize<List<Campaign>>(json, serializeOptions);
        }

        campaigns ??= new List<Campaign>();
        return campaigns;
    }
    public async Task UpdateCampaignAsync(Campaign campaign)
    {
        var campaignData = new FormUrlEncodedContent(new Dictionary<string, string>()
        {
            ["Id"] = $"{campaign.Id}",
            ["PlayerId"] = $"{campaign.PlayerId}",
            ["DungeonMasterId"] = $"{campaign.DungeonMasterId}",
            ["GameEdition"] = $"{campaign.GameEdition}",
            ["CampaignDescription"] = $"{campaign.CampaignDescription}",
            ["CampaignName"] = $"{campaign.CampaignName}"

        });

         await _client.PutAsync("Campaign/Update", campaignData);
    }

    public async Task<Campaign?> CreateCampaignAsync(Campaign campaign)
    {
        var campaignData = new FormUrlEncodedContent(new Dictionary<string, string>()
        {
            ["Id"] = $"{campaign.Id}",
            ["PlayerId"] = $"{campaign.PlayerId}",
            ["DungeonMasterId"] = $"{campaign.DungeonMasterId}",
            ["GameEdition"] = $"{campaign.GameEdition}",
            ["CampaignDescription"] = $"{campaign.CampaignDescription}",
            ["CampaignName"] = $"{campaign.CampaignName}"

        });

        var result = await _client.PostAsync("Campaign/Create", campaignData);

        if (result.IsSuccessStatusCode)
        {
            var allCampaigns = await ReadAllCampaignsAsync();
            var c = allCampaigns.FirstOrDefault(c => c.CampaignName == campaign.CampaignName);

            return c;
        }

        return null;
    }

    public async Task RemoveCampaignFromPlayerAsync(int playerId, int campainId)
    {
       await _client.DeleteAsync($"Campaign/DeletePlayer/{playerId}/{campainId}");

    }

    public async Task RemoveCampaignFromDungeonMasterAsync(int dungeonMasterId, int campainId)
    {
        await _client.DeleteAsync($"Campaign/DeleteCampagin/{dungeonMasterId}/{campainId}");

    }
}
