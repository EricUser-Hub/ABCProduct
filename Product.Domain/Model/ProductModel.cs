using System.Text.Json.Serialization;
using Product.Domain.Enums;

namespace Product.Domain.Model
{
    public class ProductModel
    {
        public ProductModel(string? din, string? name, string? shape, string? strength, LegalStatus legalStatus){
            DIN = din ?? string.Empty;
            Name = name ?? string.Empty;
            Shape = shape ?? string.Empty;
            Strength = strength ?? string.Empty;
            LegalStatus = legalStatus;
        }

        public string DIN {get; private set;}
        public string Name { get; private set; }
        public string Shape { get; private set; }
        public string Strength { get; private set; }
        
        [JsonConverter(typeof(StringToLegalStatusConverter))]
        public LegalStatus LegalStatus { get; private set; }
    }
}