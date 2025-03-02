using FluentValidation;
using FluentValidation.Validators;

namespace Product.Domain.Validators
{
    public class IdValidator<T> : PropertyValidator<T, int>
    {
        public override bool IsValid(ValidationContext<T> context, int value)
        {
            return value > 0;
        }

        public override string Name => "IdValidator";

        protected override string GetDefaultMessageTemplate(string errorCode) => "The {PropertyName} must be higher than zero.";
    }
}