namespace DnDShopWebsite.Models.Entities;

//Player entity to establish a palyer table in the db upon running a migration. 

public class Player
{
    public int Id { get; set; }
    public string FirstName { get; set; } = String.Empty;
    public string LastName { get; set; } = String.Empty;
    public int YearsOfExperiance { get; set; } = 0;
    public string Email { get; set; } = String.Empty;


    //Games of interest to the person.
    public bool DungeonAndDragons1E { get; set; }
    public bool AdvancedDnD1E { get; set; }
    public bool AdvancedDnD2E { get; set; }
    public bool DungeonAndDragons3E { get; set; }
    public bool DungeonAndDragons4E { get; set; }
    public bool DungeonAndDragons5E { get; set; }
    public ICollection<Campaign?> Campaigns { get; set; } = new List<Campaign?>();

}
