using System.ComponentModel;
using System.Reflection;

namespace Product.Domain.Enums
{
    public static class LegalStatusExtensions
    {
        public static string GetDescription(this LegalStatus value)
        {
            var type = value.GetType();
            var name = Enum.GetName(type, value);
            if (!string.IsNullOrEmpty(name))
            {
                var field = type.GetField(name);
                if (field != null)
                {
                    if (Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) is DescriptionAttribute attr)
                        return attr.Description;
                }
            }
            return string.Empty;
        }
        
        public static LegalStatus GetValueFromDescription(string description)
        {
            FieldInfo[] fields = typeof(LegalStatus).GetFields();
            var field = fields
                            .SelectMany(f => f.GetCustomAttributes(
                                typeof(DescriptionAttribute), false), (
                                    f, a) => new { Field = f, Att = a })
                            .Where(a => ((DescriptionAttribute)a.Att)
                                .Description == description).SingleOrDefault();
            return field == null ? LegalStatus.None : (LegalStatus)field.Field.GetRawConstantValue()!;
        }
    } 
}
