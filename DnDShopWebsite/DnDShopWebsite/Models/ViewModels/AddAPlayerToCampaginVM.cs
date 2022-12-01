using DnDShopWebsite.Models.Entities;

namespace DnDShopWebsite.Models.ViewModels;

public class AddAPlayerToCampaginVM
{
    public int DungeonMasterID { get; set; }
    public DungeonMaster DungeonMaster { get; set; }
    public ICollection<Player> NonPlayers { get; set; } = new List<Player>();
    public GameEdition GameEdition { get; set; }
    public string? CampaignName { get; set; } = String.Empty;
    public string? CampaignDescription { get; set; } = String.Empty;

    public ICollection<Player> CurrentPlayers = new List<Player>();
    public int PlayerId { get; set; }

}
