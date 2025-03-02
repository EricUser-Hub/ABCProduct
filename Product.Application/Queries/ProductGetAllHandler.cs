using MediatR;
using Product.Domain.Model;
using Product.Infrastructure;

namespace Product.Application.Queries 
{
    public class ProductGetAllHandler : IRequestHandler<ProductGetAllQuery, ICollection<ProductModel>>
    {
        public async Task<ICollection<ProductModel>> Handle(ProductGetAllQuery request, CancellationToken cancellationToken)
        {
            using var unitOfWork = new UnitOfWork();
            var productsFiltered = await unitOfWork.ProductRepository.GetAllFiltered(request.FilteredByShape, request.FilteredByLegalStatus);

            IOrderedEnumerable<ProductModel>? productsOrderedList = null;

            if (request.OrderedByName && request.OrderedByShape)
                productsOrderedList = productsFiltered.OrderBy(x => x.Name).ThenBy(x => x.Shape);
            else if (request.OrderedByName)
                productsOrderedList = productsFiltered.OrderBy(x => x.Name);
            else if (request.OrderedByShape)
                productsOrderedList = productsFiltered.OrderBy(x => x.Shape);
            
            return productsOrderedList?.ToList() ?? productsFiltered;
        }
    }
}