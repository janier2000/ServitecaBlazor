using System.ComponentModel;

namespace Serviteca.Frontend.Helpers
{
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
                case 1: return "Maculino";
                case 2: return "Femenino";
                case 3: return "Otros";
            }
            return null;
        }
    }
}
