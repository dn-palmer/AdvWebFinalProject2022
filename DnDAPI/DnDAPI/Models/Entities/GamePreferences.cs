using System.ComponentModel;

namespace DnDAPI.Models.Entities;
//Entitie to create a table that houses Dungeon Masters and Players prefered systems.
public class GamePreferences
{
    public int Id { get; set; }
    //can be either a DM or Players ID
    public int UserId { get; set; }
    //public DungeonMaster? DM { get; set; }
    //public Player? Player { get; set; }
    public bool? DungeonAndDragons1E { get; set; }
    public bool? AdvancedDnD1E { get; set; }
    public bool? AdvancedDnD2E { get; set; }
    public bool? DungeonAndDragons3E { get; set; }
    public bool? DungeonAndDragons4E { get; set; }
    public bool? DungeonAndDragons5E { get; set; }

}