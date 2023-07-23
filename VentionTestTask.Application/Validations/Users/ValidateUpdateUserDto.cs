using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using VentionTestTask.Domain.DTOs.Users;
using VentionTestTask.Infrastructure.IRepositories;

namespace VentionTestTask.Application.Validations.Users
{
    public class ValidateUpdateUserDto : AbstractValidator<UpdateUserDto>
    {
        public ValidateUpdateUserDto(IUserRepository userRepository) 
        {
            RuleFor(s => s.Id)
                .NotEmpty()
                .MustAsync(async (request, id, cancellationToken) =>
                    await userRepository.SelectAll().AnyAsync(u => u.Id == request.Id, cancellationToken))
                .WithMessage("User with this id is not found in the system");

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
