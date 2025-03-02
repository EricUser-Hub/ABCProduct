using MediatR;

namespace Product.Application.Queries 
{
    public class ProductGetAllHandler : IRequestHandler<ProductGetAllQuery, string>
    {
        //Pagination de la liste
        //Permettre de trier 
        //      par nom
        //      par forme
        //Permettre de filtrer 
        //      par forme 
        //      par statut légal

        public Task<string> Handle(ProductGetAllQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult("Pong");
        }
    }
}