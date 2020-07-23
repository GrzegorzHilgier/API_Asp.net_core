using System;
using System.Collections.Generic;
using System.Text;
using Domain.Entities;
using Domain.Requests.Item;
using Domain.Requests.Item.validators;
using FluentValidation.TestHelper;
using Xunit;

namespace Domain.Tests
{
    public class AddItemRequestValidatorTests
    {
        private readonly AddItemRequestValidator _validator;

        public AddItemRequestValidatorTests()
        {
            _validator = new AddItemRequestValidator();
        }

        [Fact]
        public void Should_have_error_when_ArtistId_is_null()
        {
            var addItemRequest = new AddItemRequest {Price = new Price()};
            _validator.ShouldHaveValidationErrorFor(x => x.ArtistId, addItemRequest);
            _validator.ShouldHaveValidationErrorFor(x => x.GenreId, addItemRequest);
        }

    }
}
