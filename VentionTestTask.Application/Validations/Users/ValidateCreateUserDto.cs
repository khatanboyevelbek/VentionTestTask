﻿using FluentValidation;
using VentionTestTask.Domain.DTOs;

namespace VentionTestTask.Application.Validations.Users
{
    public class ValidateCreateUserDto : AbstractValidator<CreateUserDto>
    {
        public ValidateCreateUserDto() 
        {
            RuleFor(s => s.Name).NotNull().NotEmpty()
                .WithMessage("Please provide a valid name");

            RuleFor(s => s.Email).NotNull().NotEmpty().EmailAddress()
               .WithMessage("Please provide a valid email address");

            RuleFor(s => s.Password).NotNull().Must(p => p.Length >= 8)
                .WithMessage("Password should be minumum 8 characters");

            RuleFor(s => s.Address).NotNull().NotEmpty()
                .WithMessage("Please provide a valid address");

            RuleFor(s => s.Phone).NotNull().NotEmpty()
                .WithMessage("Please provide a valid phone number");
        }
    }
}