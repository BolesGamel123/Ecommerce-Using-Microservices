using FluentValidation;
using Ordering.Application.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Validators
{
    public class CheckoutCommandValidatorV2 : AbstractValidator<CheckoutCommandV2>
    {
        public CheckoutCommandValidatorV2()
        {
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
        }
    }

}
