using FluentValidation;
using Ordering.Application.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Validators
{
    public class UpdateOrderCommandValidator:AbstractValidator<UpdateOrderCommand>
    {
        public UpdateOrderCommandValidator()
        {
            RuleFor(o => o.Id)
                  .NotEmpty()
                  .NotNull()
                  .WithMessage("Id is Required")
                  .GreaterThan(0)
                  .WithMessage("Id should not be  -ve");

            RuleFor(o => o.UserName)
                   .NotEmpty()
                   .WithMessage("UserName is Required")
                   .NotNull()
                   .MaximumLength(70)
                   .WithMessage("Username must not exceed 70 characters");

            RuleFor(o => o.TotalPrice)
                   .NotEmpty()
                   .WithMessage("TotalPrice is Required")
                   .NotNull()
                   .GreaterThan(-1)
                   .WithMessage("TotalPrice should not be  -ve");

            RuleFor(o => o.Email)
                  .NotEmpty()
                  .WithMessage("Email is Required");

            RuleFor(o => o.FirstName)
                  .NotEmpty()
                  .NotNull()
                  .WithMessage("FirstName is Required");

            RuleFor(o => o.LastName)
                 .NotEmpty()
                 .NotNull()
                 .WithMessage("LastName is Required");
        }
    }
}
