﻿using System;
using ClientApi.Models;
using FluentValidation;

namespace ClientApi.Validators
{
    public class UpdateClientModelValidator : AbstractValidator<UpdateClientModel>
    {
        public UpdateClientModelValidator()
        {
            RuleFor(x => x.FirstName)
                .NotNull()
                .WithMessage("The first name must be at least 2 character long");

            RuleFor(x => x.FirstName)
                .MaximumLength(50)
                .WithMessage("The first name must not be more than 50 characters");

            RuleFor(x => x.LastName)
                .NotNull()
                .WithMessage("The last name must be at least 2 character long");

            RuleFor(x => x.LastName)
                .MaximumLength(50)
                .WithMessage("The last name must not be more than 50 characters");

            RuleFor(x => x.MiddleName)
                .MaximumLength(50)
                .WithMessage("The middle name must not be more than 50 characters");

            RuleFor(x => x.Address)
                .MaximumLength(50)
                .WithMessage("The address must not be more than 200 characters");

            RuleFor(x => x.BirthDate)
                .InclusiveBetween(DateTime.Now.AddYears(-150).Date, DateTime.Now)
                .WithMessage("The birthday must not be longer ago than 150 years and can not be in the future");

            RuleFor(x => x.PhoneNumber)
                .Matches(@"\+?[0-9]{10}")
                .WithMessage("phone number must be 10 characters long");

            RuleFor(x => x.GenderId)
                .NotNull()
                .WithMessage("indicate your gender");
        }
    }
}
