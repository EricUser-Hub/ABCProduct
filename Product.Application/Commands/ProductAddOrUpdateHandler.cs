using MediatR;
using Product.Infrastructure;

namespace Product.Application.Commands 
{
    public class ProductAddOrUpdateHandler : IRequestHandler<ProductAddOrUpdateCommand, string>
    {
        //Pagination de la liste
        //Permettre de trier 
        //      par nom
        //      par forme
        //Permettre de filtrer 
        //      par forme 
        //      par statut légal

        //Lever un évènement sur le service bus lorsqu'un produit est ajouté ou modifié
        public Task<string> Handle(ProductAddOrUpdateCommand request, CancellationToken cancellationToken)
        {
            using var unitOfWork = new UnitOfWork();
            unitOfWork.ProductRepository.InsertOrUpdate(request.Model);
            unitOfWork.Save();
            return Task.FromResult(request.Model.DIN);
        }
    }
}