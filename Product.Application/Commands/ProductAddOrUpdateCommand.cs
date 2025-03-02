using MediatR;
using Product.Domain.Model;

namespace Product.Application.Commands 
{
    public class ProductAddOrUpdateCommand(ProductModel model) : IRequest<string>
    {
        public ProductModel Model { get; private set; } = model;
    }

}