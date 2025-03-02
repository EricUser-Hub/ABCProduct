using Product.Domain.Enums;
using Product.Domain.Model;

namespace Product.API.Reply
{
    public class ProductReply(ProductModel model)
    {
        public string DIN { get; private set; } = model.DIN;
        public string Name { get; private set; } = model.Name;
        public string Shape { get; private set; } = model.Shape;
        public string Strength { get; private set; } = model.Strength;

        public string LegalStatus { get; private set; } = model.LegalStatus.GetDescription();
    }
}