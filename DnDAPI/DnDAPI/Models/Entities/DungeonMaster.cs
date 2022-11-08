namespace DnDAPI.Models.Entities;

//Entitie for the dungen master entires in the db to establish the data needed for the table to be sucessful. 
public class DungeonMaster
{
    public int Id { get; set; }
    public string FirstName { get; set; } = String.Empty;
    public string LastName { get; set; } = String.Empty;
    public int YearsOfExperiance { get; set; } = 0;
    public string Email { get; set; } =String.Empty;
    public bool? DungeonAndDragons1E { get; set; }
    public bool? AdvancedDnD1E { get; set; }
    public bool? AdvancedDnD2E { get; set; }
    public bool? DungeonAndDragons3E { get; set; }
    public bool? DungeonAndDragons4E { get; set; }
    public bool? DungeonAndDragons5E { get; set; }
    public ICollection<Campaign?> Campaigns { get; set; } = new List<Campaign?>();

}
