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

            if (string.IsNullOrWhiteSpace(name))
                throw new ValidationException($"The '{nameof(name).ToUpper()}' is required.");
            if (name.Length > 30)
                throw new ValidationException($"The max length for the '{nameof(name).ToUpper()}' is 30 characters.");
            if (string.IsNullOrWhiteSpace(shape))
                throw new ValidationException($"The '{nameof(shape).ToUpper()}' is required.");
            if (shape.Length > 100)
                throw new ValidationException($"The max length for the '{nameof(shape).ToUpper()}' is 100 characters.");
            if (string.IsNullOrWhiteSpace(strength))
                throw new ValidationException($"The '{nameof(strength).ToUpper()}' is required.");
            if (strength.Length > 100)
                throw new ValidationException($"The max length for the '{nameof(strength).ToUpper()}' is 100 characters.");
            
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