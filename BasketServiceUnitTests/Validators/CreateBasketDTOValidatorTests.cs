using System.Collections.Generic;
using BasketService.DTOs;
using BasketService.Validators;
using FluentAssertions;
using Xunit;

namespace BasketServiceUnitTests.Validators
{
    public class CreateBasketDTOValidatorTests
    {
        private readonly CreateBasketDTOValidator _validator;
        private readonly CreateBasketDTO _DTO;

        public CreateBasketDTOValidatorTests()
        {
            _validator = new CreateBasketDTOValidator();
            _DTO = new CreateBasketDTO
            {
                Products = new List<CreatBasketItemDTO>
                {
                    new CreatBasketItemDTO()
                }
            };
        }

        [Fact(DisplayName = "Given model is valid when validate is invoked then validation should pass")]
        public void Validate_ModelIsValid_ThenValidationSucceeds()
        {
            var result = _validator.Validate(_DTO);
            result.IsValid.Should().BeTrue();
        }

        [Fact(DisplayName = "Given model is null when validate is invoked then validation should fail")]
        public void Validate_NullModel_ThenValidationFails()
        {
            var result = _validator.Validate((CreateBasketDTO)null);

            result.IsValid.Should().BeFalse();
        }

        [Fact(DisplayName = "Given products is empty when validate is invoked then validation should fail")]
        public void Validate_EmptyProducts_ThenValidationFails()
        {
            _DTO.Products = new List<CreatBasketItemDTO>();

            var result = _validator.Validate(_DTO);

            result.IsValid.Should().BeFalse();
        }
    }
}
