using System.ComponentModel.DataAnnotations;
using Product.Domain.Enums;

namespace Product.API.Request
{
    public class GetAllProductsRequest 
    {
        public required int PageNumber { get; set; } = 1;
        
        public required int PageSize { get; set; } = 20;

        public required bool OrderedByName { get; set; }

        public required bool OrderedByShape { get; set; }

        public string? FilteredByShape { get; set; }

        public LegalStatus? FilteredByLegalStatus { get; set; }

        public bool IsValid() 
        {
            if (PageNumber < 1)
                throw new ValidationException($"'{nameof(PageNumber)}' must be 1 or more.");
            if (PageSize < 1)
                throw new ValidationException($"'{nameof(PageSize)}' must be 1 or more.");

            return true;
        }
    }
}