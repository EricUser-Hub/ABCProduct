using MediatR;
using Product.Domain.Model;
using Product.Infrastructure;

namespace Product.Application.Queries 
{
    public class ProductGetHandler : IRequestHandler<ProductGetQuery, ProductModel?>
    {
        public async Task<ProductModel?> Handle(ProductGetQuery request, CancellationToken cancellationToken)
        {
            using var unitOfWork = new UnitOfWork();
            return await unitOfWork.ProductRepository.GetByDINAsync(request.ProductDIN);
        }
    }
}