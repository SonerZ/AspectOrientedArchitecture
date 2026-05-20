using FluentValidation;
using Application.Entities.CustomEntities.User;

namespace Application.Business.ValidationRules.FluentValidation
{
    public class UserValidator : AbstractValidator<UserCreateDto>
    {
        public UserValidator()
        {
            RuleFor(i=> i.Username)
            .MinimumLength(5)
            .MaximumLength(60)
            .NotEmpty();

            RuleFor(i=> i.Password)
            .MinimumLength(5)
            .MaximumLength(60)
            .NotEmpty();
           
            RuleFor(user=> user)
            .Must(ArePasswordsMatching)
             .WithMessage("Passwords must match.");
        }

    private bool ArePasswordsMatching(UserCreateDto user)
    {
        return user.Password == user.ConfirmPassword;
    }
    }
}
