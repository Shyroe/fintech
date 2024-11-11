using System.ComponentModel.DataAnnotations;
using System;
using System.Linq;
using System.Reflection;

namespace FinTech.App.Extensions
{
    public static class EnumHelper
    {
        public static string GetDisplayName(this Enum enumValue)
        {
            var displayAttribute = enumValue.GetType()
                .GetMember(enumValue.ToString())
                .FirstOrDefault()?
                .GetCustomAttribute<DisplayAttribute>();

            return displayAttribute?.GetName() ?? enumValue.ToString();
        }
    }
}
