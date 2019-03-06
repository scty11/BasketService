using BasketService.DTOs;
using FluentValidation;
using FluentValidation.Results;

namespace BasketService.Validators
{
    public class CreateBasketDTOValidator : AbstractValidator<CreateBasketDTO>
    {
        public CreateBasketDTOValidator()
        {
            RuleFor(cb => cb.Products)
                .NotEmpty();
        }

        protected override bool PreValidate(ValidationContext<CreateBasketDTO> context, ValidationResult result)
        {
            if (context.InstanceToValidate != null) return true;
            result.Errors.Add(new ValidationFailure("", $"{nameof(CreateBasketDTO)} must not be null"));
            return false;
        }
    }
}
