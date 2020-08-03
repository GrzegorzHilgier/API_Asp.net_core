using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;

namespace Domain.Requests.Artist.Validators
{
    public class AddArtistRequestValidator : AbstractValidator<AddArtistRequest>
    {
        public AddArtistRequestValidator()
        {
            RuleFor(artist => artist.ArtistName).NotEmpty();
        }
    }
}
