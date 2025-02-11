using System.ComponentModel;
using System.Reflection;

namespace BobrVerse.Common.Extenions
{
    public static class EnumExtenions
    {
        public static string GetDescription(this Enum value)
        {
            var field = value.GetType().GetField(value.ToString());
            var attribute = field?.GetCustomAttribute<DescriptionAttribute>();

            return attribute?.Description ?? value.ToString();
        }
    }
}
