using FluentValidation;
using Product.Domain.Model;

namespace Product.Domain.Validators 
{
    public class ProductValidator : AbstractValidator<ProductModel>
    {
        public ProductValidator(IdValidator<ProductModel> idValidator)
        {
            //RuleFor(product => product.Id).SetValidator(idValidator);
        }
    }
}