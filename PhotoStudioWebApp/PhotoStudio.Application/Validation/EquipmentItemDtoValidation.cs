using FluentValidation;
using PhotoStudio.Domain.Entities;

namespace PhotoStudio.Application.Validation
{
    public class EquipmentItemDtoValidation : AbstractValidator<EquipmentItemDto>
    {
        public EquipmentItemDtoValidation()
        {
            RuleFor(e => e.Name)
                .NotEmpty()
                .WithMessage("Name is required.")
                .Length(1, 50)
                .WithMessage("Name length should be greater than 1 and less than 50");
            RuleFor(e => e.Description)
                .NotEmpty()
                .WithMessage("Description is required.")
                .Length(10, 5000)
                .WithMessage("Description should be greater than 10 and less than 5000");
        }
    }
}
