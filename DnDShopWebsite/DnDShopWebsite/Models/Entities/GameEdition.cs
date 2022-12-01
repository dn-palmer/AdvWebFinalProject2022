using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace DnDShopWebsite.Models.Entities;

//truly do not remember why I made this? For a view model later mostlikley! Oh yeaaaah!!!! Campains can have a drop down and use this when I am building the
//main app. The campagin entity has a property that uses this enum as its data type big ole blank moment from me a moment ago. 
public enum GameEdition
{
    [Description("Dungeons & Dragons 1st Edition")] DnD1E,
    [Description("Advanced Dungeons & Dragons 1st Edition")] ADnD1E,
    [Description("Advanced Dungeons & Dragons 2nd Edition")] ADnD2E,
    [Description("Dungeons & Dragons 2nd Edition")] DnD3E,
    [Description("Dungeons & Dragons 4th Edition")] DnD4E,
    [Description("Dungeons & Dragons 5th Edition")] DnD5E
}

//This core idea came from https://stackoverflow.com/questions/61953/how-do-you-bind-an-enum-to-a-dropdownlist-control-in-asp-net/61961
public static class GameEditionDescription
{
    public static string Description(this System.Enum value)
    {
        var attributes = (DescriptionAttribute[])value.GetType().GetField(value.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), false);
        return attributes.Length > 0 ? attributes[0].Description : value.ToString();
    }

}


// More help from https://www.c-sharpcorner.com/code/323/how-do-i-get-description-of-enum.aspx
//Need to actually figure out how this works the tutorial just gave the code. 

public static class EnumDescriptionRetreval
{
    public static string GetEnumDescription(this Enum enumValue)
    {
        var fieldInfo = enumValue.GetType().GetField(enumValue.ToString());

        var descriptionAttributes = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);

        return descriptionAttributes.Length > 0 ? descriptionAttributes[0].Description : enumValue.ToString();
    }
}
