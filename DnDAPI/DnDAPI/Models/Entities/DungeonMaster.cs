namespace DnDAPI.Models.Entities;

//Entitie for the dungen master entires in the db to establish the data needed for the table to be sucessful. 
public class DungeonMaster
{
    public int Id { get; set; }
    public string FirstName { get; set; } = String.Empty;
    public string LastName { get; set; } = String.Empty;
    public int YearsOfExperiance { get; set; } = 0;
    public string Email { get; set; } =String.Empty;
    public GamePreferences? GamePreferences { get; set; }
    public ICollection<Player?> Players { get; set; } = new List<Player?>();

}
