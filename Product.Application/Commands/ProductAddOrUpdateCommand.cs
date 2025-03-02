using MediatR;
using Product.Domain.Model;

namespace Product.Application.Commands 
{
    public class ProductAddOrUpdateCommand(ProductModel model) : IRequest<string>
    {
        // TODO Eric : Ajouter validation ICI pour le productDIN venant du domain, réutilisation des validators
        public ProductModel Model { get; private set; } = model;
    }

}