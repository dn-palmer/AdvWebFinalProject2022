using System.ComponentModel;

namespace DnDAPI.Models.Entities;
//Entitie to create a table that houses Dungeon Masters and Players prefered systems.
public class Campaign
{
    public int Id { get; set; }
    public GameEdition GameEdition { get; set; } 
    public string CampaignName { get; set; } = String.Empty;  
    public string CampaignDescription { get; set; } = String.Empty;    
    public int DungeonMasterId { get; set; }
    public DungeonMaster? DungeonMaster { get; set; }
    public int PlayerId { get; set; }
    public Player? Player { get; set; }



}