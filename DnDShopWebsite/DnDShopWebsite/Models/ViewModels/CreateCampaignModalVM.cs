using DnDShopWebsite.Models.Entities;

namespace DnDShopWebsite.Models.ViewModels;

public class CreateCampaignModalVM
{
    public ICollection<DungeonMaster> DungeonMasters { get; set; } = new List<DungeonMaster>();
    public ICollection<Player> Players { get; set; } = new List<Player>();
    public GameEdition GameEdition { get; set; }
    public string? CampaignName { get; set; } = String.Empty;
    public string? CampaignDescription { get; set; } = String.Empty;
    public int PlayerId { get; set; }
}
