using DnDShopWebsite.Models.Entities;

namespace DnDShopWebsite.Models.ViewModels;

public class CampaignCardPartialVM
{
    public ICollection<Player> Players { get; set; } = new List<Player>();
    public ICollection<Campaign> AllCampaigns { get; set; } = new List<Campaign>();
    public string? CurrentCampaignName { get; set; } = string.Empty;
    public ICollection<DungeonMaster> DungeonMasters { get; set; } = new List<DungeonMaster>();
}
