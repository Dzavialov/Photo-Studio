using FluentValidation;
using FluentValidation.AspNetCore;
using PhotoStudio.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoStudio.Application.Validation
{
    public class RegisterDtoValidation : AbstractValidator<RegisterDto>
    {
        public RegisterDtoValidation()
        {
            RuleFor(r => r.Username)
                .NotEmpty()
                .WithMessage("Username is required.")
                .Length(4, 15)
                .WithMessage("Username length must be greater than 4 and less than 15");
            RuleFor(r => r.Email)
                .NotEmpty()
                .WithMessage("Email is required.")
                .EmailAddress()
                .WithMessage("Wrong email format.");
            RuleFor(r => r.Password)
                .NotEmpty()
                .WithMessage("Password is required.")
                .Length(8, 50)
                .WithMessage("Password length must be greater than 8 and less than 50.")
                .Matches("[A-Z]")
                .WithMessage("Password must include at least one uppercase letter.")
                .Matches("[a-z]")
                .WithMessage("Password must include at least one lowercase letter.")
                .Matches("[0-9]")
                .WithMessage("Password must include at least one number.");
        }
    }
}
