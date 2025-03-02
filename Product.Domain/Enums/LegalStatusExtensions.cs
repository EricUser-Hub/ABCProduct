using System.ComponentModel;

namespace Product.Domain.Enums
{
    public static class LegalStatusExtensions
    {
        public static string GetDescription(this LegalStatus val)
        {
            var attributes = val.GetType().GetField(val.ToString())?.GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (attributes != null && attributes.Length > 0)
                return  ((DescriptionAttribute)attributes[0]).Description;

            return string.Empty;
        }

        public static LegalStatus GetValueFromDescription(string? description)
        {
            if (string.IsNullOrEmpty(description))
                return LegalStatus.None;

            foreach(var field in typeof(LegalStatus).GetFields())
            {
                if (Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) is DescriptionAttribute attribute)
                {
                    if (attribute.Description == description)
                        return (LegalStatus)field.GetValue(null)!;
                }
                else
                {
                    if (field.Name == description)
                        return (LegalStatus)field.GetValue(null)!;
                }
            }

            return LegalStatus.None;
        }
    } 
}
