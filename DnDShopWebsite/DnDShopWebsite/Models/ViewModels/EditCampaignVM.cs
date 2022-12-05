using DnDShopWebsite.Models.Entities;

namespace DnDShopWebsite.Models.ViewModels;

public class EditCampaignVM
{
    public DungeonMaster DungeonMaster { get; set; }
    public ICollection<Player> CurrentPlayers { get; set; }
    public string CampaignName { get; set; }

    public int CampaignId { get; set; }
    public string CampaignDescription { get; set;}
    public GameEdition GameEdition { get; set; }
}
