using FluentValidation;
using StockWebApp1.DTO;

namespace StockWebApp1.Validators
{
    public class RegistrationValidator : AbstractValidator<RegisterDto>
    {
        public RegistrationValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().WithMessage("Username is required.");
            RuleFor(x => x.PasswordHash).NotEmpty().WithMessage("Password is required.");
            RuleFor(x => x.PasswordHash).MinimumLength(6).WithMessage("Password must be at least 6 characters long.");
        }
    }
}
