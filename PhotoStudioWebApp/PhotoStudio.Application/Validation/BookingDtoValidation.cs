using FluentValidation;
using PhotoStudio.Application.DTOs;

namespace PhotoStudio.Application.Validation
{
    public class BookingDtoValidation : AbstractValidator<BookingDto>
    {
        public BookingDtoValidation()
        {
            RuleFor(b => b.BookFrom)
                .NotEmpty()
                .WithMessage("Date is required.")
                .GreaterThanOrEqualTo(DateTime.Today)
                .WithMessage("Date should be greater than or equal to today.");
            RuleFor(b => b.BookTo)
                .NotEmpty()
                .WithMessage("Date is required.")
                .GreaterThanOrEqualTo(b => b.BookFrom)
                .WithMessage("Ending time should be greater than start time.")
                .LessThanOrEqualTo(b => b.BookFrom.AddHours(5));
        }
    }
}
