using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Product.Domain.Enums;

namespace Product.Domain.Model
{
    public class ProductModel
    {
        private ProductModel(string? din, string? name, string? shape, string? strength)
        {
            if (string.IsNullOrEmpty(din))
                throw new ValidationException($"The '{nameof(din).ToUpper()}' is required.");
            if (din.Length != 8)
                throw new ValidationException($"The '{nameof(din).ToUpper()}' must have 8 characters.");
            if (!int.TryParse(din, out _))
                throw new ValidationException($"The '{nameof(din).ToUpper()}' must be a number.");

            if (string.IsNullOrEmpty(name))
                throw new ValidationException($"The '{nameof(name).ToUpper()}' is required.");
            if (string.IsNullOrEmpty(shape))
                throw new ValidationException($"The '{nameof(shape).ToUpper()}' is required.");
            if (string.IsNullOrEmpty(strength))
                throw new ValidationException($"The '{nameof(strength).ToUpper()}' is required.");
            
            DIN = din ?? string.Empty;
            Name = name ?? string.Empty;
            Shape = shape ?? string.Empty;
            Strength = strength ?? string.Empty;
        }

        public ProductModel(string? din, string? name, string? shape, string? strength, string? legalStatus) :
            this(din, name, shape, strength)
        {
            if (string.IsNullOrEmpty(legalStatus))
                throw new ValidationException($"The '{nameof(legalStatus).ToUpper()}' is required.");
            LegalStatus = LegalStatusExtensions.GetValueFromDescription(legalStatus);
        }

        [JsonConstructor]
        public ProductModel(string? din, string? name, string? shape, string? strength, LegalStatus legalStatus) :
            this(din, name, shape, strength)
        {
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