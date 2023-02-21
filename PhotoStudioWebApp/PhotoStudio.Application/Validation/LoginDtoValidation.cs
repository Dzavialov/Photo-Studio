using FluentValidation;
using PhotoStudio.Application.DTOs;

namespace PhotoStudio.Application.Validation
{
    public class LoginDtoValidation : AbstractValidator<LoginDto>
    {
        public LoginDtoValidation()
        {
            RuleFor(l => l.Username)
                .NotEmpty()
                .WithMessage("Username is required.");
            RuleFor(l => l.Password)
                .NotEmpty()
                .WithMessage("Password is required.");
        }
    }
}
