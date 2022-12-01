using DnDShopWebsite.Models.Entities;

namespace DnDShopWebsite.Models.ViewModels
{
    public class DeleteCampaignVM
    {
        public int CampaignId { get; set; }
        public int DungeonMasterId { get; set; }

        public ICollection<Player> AllPlayersInCampaign = new List<Player>();
        public Campaign Campaign { get; set; }
        public DungeonMaster DungeonMaster { get; set; }
        
    }
}
