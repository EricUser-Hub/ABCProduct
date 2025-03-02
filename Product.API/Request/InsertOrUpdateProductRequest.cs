using Product.Domain.Model;

namespace Product.API.Request
{
    public class InsertOrUpdateProductRequest 
    {
        public required string DIN { get; set; }
        public required string Name { get; set; }
        public required string Shape { get; set; }
        public required string Strength { get; set; }
        
        public required string LegalStatus { get; set; }

        public ProductModel ToDomain() {
            return new ProductModel(DIN, Name.ToUpper(), Shape.ToUpper(), Strength.ToUpper(), LegalStatus);
        }
    }
}