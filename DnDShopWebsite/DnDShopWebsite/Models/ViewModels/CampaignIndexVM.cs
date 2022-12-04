using DnDShopWebsite.Models.Entities;

namespace DnDShopWebsite.Models.ViewModels;

public class CampaignIndexVM
{
    public ICollection<Player> Players { get; set; } = new List<Player>();
    public ICollection<Campaign> AllCampaigns { get; set; } = new List<Campaign>();
    public ICollection<string> UniqeCampaignNames { get; set; } = new List<string>();
    public ICollection<DungeonMaster> DungeonMasters { get; set;} = new List<DungeonMaster>();      
    public CreateCampaignModalVM CampaignCreationModal { get; set; } = new CreateCampaignModalVM();
    public CampaignCardPartialVM CampaignPartialVM { get; set; } = new CampaignCardPartialVM();
}
