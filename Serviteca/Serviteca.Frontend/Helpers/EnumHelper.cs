using System.ComponentModel;

namespace Serviteca.Frontend.Helpers;

public class EnumHelper
{
    public static string GetEnumDescription(Enum value)
    {
        var field = value.GetType().GetField(value.ToString())!;
        var attributes = (DescriptionAttribute[])field.GetCustomAttributes(typeof(DescriptionAttribute), false);

        if (attributes.Length > 0)
        {
            return attributes[0].Description;
        }
        else
        {
            return value.ToString();
        }
    }

    public static string GetGender(int value)
    {
        switch (value)
        {
            case 0: return "Masculino";
            case 1: return "Femenino";
            case 2: return "Otros";
        }
        return "";
    }

    public static string GetStatus(int value)
    {
        switch (value)
        {
            case 0: return "Vigente";
            case 1: return "Vencido";
        }
        return "";
    }
}