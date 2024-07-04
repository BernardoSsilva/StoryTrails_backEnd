using FluentValidation;
using StoryTrails.Communication.Request;

namespace StoryTrails.Application.Validators
{
    public class UserValidator : AbstractValidator<UserJsonRequest>
    {
        public UserValidator()
        {
            RuleFor(user => user.UserPassword).MinimumLength(8).WithMessage("Password must be grater or equal than 8 characters");
            RuleFor(user => user.Username).NotEmpty().WithMessage("User name is required");
            RuleFor(user => user.UserLogin).NotEmpty().WithMessage("User login is required");
        }
    }
}
