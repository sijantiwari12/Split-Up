using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace SplitUp.Core.Validators
{
    public class UserRegisterValidator : AbstractValidator<User>
    {
        public UserRegisterValidator()
        {
            RuleFor(x => x.Email).NotNull().EmailAddress();
            RuleFor(x => x.FullName).NotNull();
            RuleFor(x => x.Password).NotNull().Length(6, 30);
            RuleFor(x => x.Gender).NotNull();
        }
    }
}
