namespace DnDShopWebsite.Models.ViewModels;

public class RemovePlayerVM
{
    public int CampaignId { get; set; }
    public string CampaignName { get; set;} = string.Empty;
    public int PlayerId { get; set; }
    public string PlayerFullName { get; set; } = string.Empty;

}
