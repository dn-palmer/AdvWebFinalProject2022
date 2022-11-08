namespace DnDAPI.Models.Entities;

//Player entitie to establish a palyer table in the db upon running a migration. 
public class Player
{
    public int Id { get; set; }
    public string FirstName { get; set; } = String.Empty;
    public string LastName { get; set; } = String.Empty;
    public int YearsOfExperiance { get; set; } = 0;
    public string Email { get; set; } = String.Empty;
    public GamePreferences? GamePreferences { get; set; }
    public int? DungeonMasterId { get; set; }
    public DungeonMaster? DungeonMaster { get; set; }

}
