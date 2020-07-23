﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Domain.Requests.Artist;
using Domain.Requests.Genre;
using Domain.Services;
using FluentValidation;

namespace Domain.Requests.Item.validators
{
    public class EditItemRequestValidator : AbstractValidator<EditItemRequest>
    {
        private readonly IArtistService _artistService;
        private readonly IGenreService _genreService;
        public EditItemRequestValidator(IArtistService artistService, IGenreService genreService)
        {
            _artistService = artistService;
            _genreService = genreService;
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.GenreId).NotEmpty();
            RuleFor(x => x.ArtistId).NotEmpty();
            RuleFor(x => x.Price).NotEmpty();
            RuleFor(x => x.Price).Must(x => x?.Amount > 0);
            RuleFor(x => x.ReleaseDate).NotEmpty();
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.ArtistId)
                .NotEmpty()
                .MustAsync(ArtistExists).WithMessage("Artist must exists");
            RuleFor(x => x.GenreId)
                .NotEmpty()
                .MustAsync(GenreExists).WithMessage("Genre must exists");
        }

        private async Task<bool> ArtistExists(Guid artistId, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(artistId.ToString()))
            {
                return false;
            }

            var artist = await _artistService.GetArtistAsync(new GetArtistRequest {Id = artistId});
            return artist != null;
        }

        private async Task<bool> GenreExists(Guid genreId, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(genreId.ToString()))
            {
                return false;
            }

            var artist = await _genreService.GetGenreAsync(new GetGenreRequest {Id = genreId});
            return artist != null;
        }
    }
}