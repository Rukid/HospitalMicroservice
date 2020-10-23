using System;
using DoctorApi.Models;
using FluentValidation;

namespace DoctorApi.Validators
{
    public class UpdateDoctorModelValidator : AbstractValidator<UpdateDoctorModel>
    {
        public UpdateDoctorModelValidator()
        {
            RuleFor(x => x.FirstName)
                .NotNull()
                .WithMessage("The first name must be at least 2 character long");

            RuleFor(x => x.FirstName)
                .MaximumLength(50)
                .WithMessage("The first name must be less than 50 characters");

            RuleFor(x => x.LastName)
                .NotNull()
                .WithMessage("The last name must be at least 2 character long");

            RuleFor(x => x.LastName)
                .MaximumLength(50)
                .WithMessage("The last name must be less than 50 characters");

            RuleFor(x => x.MiddleName)
                .MaximumLength(50)
                .WithMessage("The middle name must be less than 50 characters");

            RuleFor(x => x.Speciality)
                .MaximumLength(50)
                .WithMessage("The address must be less than 30 characters");

            RuleFor(x => x.BirthDate)
                .InclusiveBetween(DateTime.Now.AddYears(-150).Date, DateTime.Now)
                .WithMessage("The birthday must not be longer ago than 150 years and can not be in the future");
        }
    }
}
