using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Product.Domain.Enums
{
    public static class LegalStatusExtensions
    {
        /*
        public static string GetDescription(this LegalStatus val)
        {
            var attributes = val.GetType().GetField(val.ToString())?.GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (attributes != null && attributes.Length > 0)
                return  ((DescriptionAttribute)attributes[0]).Description;

            return string.Empty;
        }
        */
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


/*
        public static LegalStatus GetValueFromDescription(string? description)
        {
            if (string.IsNullOrEmpty(description))
                return LegalStatus.None;


            var valuesToCompare = Enum.GetValues(typeof(LegalStatus));

            return 
                .Cast<LegalStatus>()
                .FirstOrDefault(v => string.Compare(v.GetDescription(), description, true) == 0);

            


            foreach(var field in typeof(LegalStatus).GetFields())
            {
                if (Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) is DescriptionAttribute attribute)
                {
                    if (string.Compare(attribute.Description, description, true) == 0)
                    {   
                        var tmp = (LegalStatus)field.GetValue(null)!;
                        return tmp;
                    }
                }
                else
                {
                    if (string.Compare(field.Name, description, true) == 0)
                    {   
                        var tmp = (LegalStatus)field.GetValue(null)!;
                        return tmp;
                    }
                }
            }
            

            throw new ValidationException("No Legaltatus was found that match the value inserted.");
        }
        */
    } 
}
