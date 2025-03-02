using MediatR;
using Product.Domain.Model;

namespace Product.Application.Queries 
{
    public class ProductGetQuery(string productDIN) : IRequest<ProductModel?>
    {
        // TODO Eric : Ajouter validation ICI pour le productDIN venant du domain, réutilisation des validators
        public string ProductDIN { get; private set; } = productDIN;
    }

}