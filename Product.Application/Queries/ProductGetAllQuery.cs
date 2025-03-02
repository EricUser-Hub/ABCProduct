using MediatR;

namespace Product.Application.Queries 
{
    public class ProductGetAllQuery : IRequest<string>
    {
        //Pagination de la liste
        //Permettre de trier 
        //      par nom
        //      par forme
        //Permettre de filtrer 
        //      par forme 
        //      par statut légal
    }

}