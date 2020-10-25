using FluentValidation;
using VisitApi.Models;

namespace VisitApi.Validators
{
    public class VisitModelValidator : AbstractValidator<VisitModel>
    {
        public VisitModelValidator()
        {
            RuleFor(x => x.ClientFullName)
                .NotNull()
                .WithMessage("The client name must be at least 2 character long");
            RuleFor(x => x.ClientFullName)
                .MinimumLength(2).WithMessage("The client name must be at least 2 character long");

            RuleFor(x => x.DoctorFullName)
                .NotNull()
                .WithMessage("The doctor name must be at least 2 character long");
            RuleFor(x => x.DoctorFullName)
                .MinimumLength(2).WithMessage("The doctor name must be at least 2 character long");
        }
    }
}