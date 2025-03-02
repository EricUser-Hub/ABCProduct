using MediatR;
using Product.Domain.Enums;
using Product.Domain.Model;

namespace Product.Application.Queries 
{
    public class ProductGetAllQuery(bool orderedByName, bool orderedByShape, string? filteredByShape, LegalStatus? filteredByLegalStatus) : IRequest<ICollection<ProductModel>>
    {
        public bool OrderedByName { get; set; } = orderedByName;

        public bool OrderedByShape { get; set; } = orderedByShape;

        public string? FilteredByShape { get; set; } = filteredByShape?.ToUpper();

        public LegalStatus? FilteredByLegalStatus { get; set; } = filteredByLegalStatus;
    }

}