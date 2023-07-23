using FluentValidation;
using VentionTestTask.Domain.DTOs.Users;

namespace VentionTestTask.Application.Validations.Users
{
    public class ValidateCreateUserDto : AbstractValidator<CreateUserDto>
    {
        public ValidateCreateUserDto() 
        {
            RuleFor(s => s.Name)
                .NotNull()
                .NotEmpty();

            RuleFor(s => s.Email)
                .NotNull()
                .NotEmpty()
                .EmailAddress();

            RuleFor(s => s.Password)
                .NotNull()
                .Must(p => p.Length >= 8)
                .WithMessage("Password should contain at least 8 characters");

            RuleFor(s => s.Address)
                .NotNull()
                .NotEmpty();

            RuleFor(s => s.Phone)
                .NotNull()
                .NotEmpty();
        }
    }
}
