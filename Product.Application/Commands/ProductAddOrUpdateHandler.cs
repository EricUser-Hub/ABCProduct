using MediatR;
using Product.Infrastructure;

namespace Product.Application.Commands 
{
    public class ProductAddOrUpdateHandler : IRequestHandler<ProductAddOrUpdateCommand, string>
    {
        public Task<string> Handle(ProductAddOrUpdateCommand request, CancellationToken cancellationToken)
        {
            using var unitOfWork = new UnitOfWork();
            unitOfWork.ProductRepository.InsertOrUpdate(request.Model);
            unitOfWork.Save();
            
            return Task.FromResult(request.Model.DIN);
        }
    }
}