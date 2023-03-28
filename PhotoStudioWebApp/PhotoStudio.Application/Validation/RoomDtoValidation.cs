using FluentValidation;
using PhotoStudio.Domain.Entities;

namespace PhotoStudio.Application.Validation
{
    public class RoomDtoValidation : AbstractValidator<RoomDto>
    {
        public RoomDtoValidation()
        {
            RuleFor(r => r.Name)
                .NotEmpty()
                .WithMessage("Name is required.")
                .Length(1, 50)
                .WithMessage("Name length should be greater than 1 and less than 50");
            //RuleFor(r => r.Image)
            //    .NotEmpty()
            //    .WithMessage("Image is required.");
            RuleFor(r => r.Description)
                .NotEmpty()
                .WithMessage("Description is required.")
                .Length(10, 5000)
                .WithMessage("Description should be greater than 10 and less than 5000");
            RuleFor(r => r.AdditionalInformation)
                .NotEmpty()
                .WithMessage("Additional information is required.")
                .Length(10, 1000)
                .WithMessage("Description should be greater than 10 and less than 1000");
        }
    }
}
