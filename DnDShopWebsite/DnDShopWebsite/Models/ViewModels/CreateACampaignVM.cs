using DnDShopWebsite.Models.Entities;

namespace DnDShopWebsite.Models.ViewModels;

public class CreateACampaignVM
{
    public int DungeonMasterID { get; set; }
    public DungeonMaster DungeonMaster { get; set; }
    public ICollection<Player> Players { get; set; } = new List<Player>();
    public GameEdition GameEdition { get; set; }
    public string? CampaignName { get; set; } = String.Empty;
    public string? CampaignDescription { get; set; } = String.Empty;
    public int PlayerId { get; set; }
}
