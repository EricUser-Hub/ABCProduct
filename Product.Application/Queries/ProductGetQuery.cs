using System.ComponentModel.DataAnnotations;
using MediatR;
using Product.Domain.Model;

namespace Product.Application.Queries 
{
    public class ProductGetQuery : IRequest<ProductModel?>
    {
        public ProductGetQuery(string productDIN)
        {
            if (string.IsNullOrEmpty(productDIN))
                throw new ValidationException("The 'DIN' is required.");
            if (productDIN.Length != 8)
                throw new ValidationException("The 'DIN' must have 8 characters.");
            if (!int.TryParse(productDIN, out _))
                throw new ValidationException("The 'DIN' must be a number.");

            ProductDIN = productDIN;
        }
        
        public string ProductDIN { get; private set; }
    }

}