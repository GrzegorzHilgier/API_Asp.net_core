using System;
using System.Collections.Generic;
using System.Text;
using Domain.Entities;
using Domain.Entities.Catalog;
using Domain.Requests.Artist;
using Domain.Requests.Genre;
using Domain.Requests.Item;
using Domain.Requests.Item.Validators;
using Domain.Responses.Item;
using Domain.Services;
using FluentValidation.TestHelper;
using Moq;
using Xunit;

namespace Domain.Tests
{
    public class AddItemRequestValidatorTests
    {
        private readonly Mock<IArtistService> _artistServiceMock;
        private readonly Mock<IGenreService> _genreServiceMock;
        private readonly AddItemRequestValidator _validator;

        public AddItemRequestValidatorTests()
        {
            _artistServiceMock = new Mock<IArtistService>();
            _artistServiceMock
                .Setup(x => x.GetArtistAsync(It.IsAny<GetArtistRequest>()))
                .ReturnsAsync(() => new ArtistResponse());
            _genreServiceMock = new Mock<IGenreService>();
            _genreServiceMock
                .Setup(x => x.GetGenreAsync(It.IsAny<GetGenreRequest>()))
                .ReturnsAsync(() => new GenreResponse());
            _validator = new AddItemRequestValidator(_artistServiceMock.Object, _genreServiceMock.Object);
        }

        [Fact]
        public void Should_have_error_when_ArtistId_is_null()
        {
            var addItemRequest = new AddItemRequest {Price = new Price()};
            _validator.ShouldHaveValidationErrorFor(x => x.ArtistId, addItemRequest);
            _validator.ShouldHaveValidationErrorFor(x => x.GenreId, addItemRequest);
        }

        [Fact]
        public void should_have_error_when_ArtistId_doesnt_exist()
        {
            _artistServiceMock
                .Setup(x => x.GetArtistAsync(It.IsAny<GetArtistRequest>()))
                .ReturnsAsync(() => null);
            var addItemRequest = new AddItemRequest { Price = new Price(),
                ArtistId = Guid.NewGuid() };
            _validator.ShouldHaveValidationErrorFor(x => x.ArtistId,
                addItemRequest);
        }

        [Fact]
        public void should_have_error_when_GenreId_doesnt_exist()
        {
            _genreServiceMock
                .Setup(x => x.GetGenreAsync(It.IsAny<GetGenreRequest>()))
                .ReturnsAsync(() => null);
            var addItemRequest = new AddItemRequest { Price = new Price(),
                GenreId = Guid.NewGuid() };
            _validator.ShouldHaveValidationErrorFor(x => x.GenreId,
                addItemRequest);
        }

    }
}
