using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace SplitUp.Core.Validators
{
    public class UserLoginValidator : AbstractValidator<User>
    {
        public UserLoginValidator()
        {
            RuleFor(user => user.Email).NotNull().EmailAddress();
            RuleFor(user => user.Password).NotNull();
        }
    }
}
