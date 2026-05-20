using FluentValidation;
using Application.Entities.CustomEntities.User;

namespace Application.Business.ValidationRules.FluentValidation
{
    public class UserLoginValidator : AbstractValidator<UserLoginDto>
    {
        public UserLoginValidator()
        {
            RuleFor(i=> i.Username)
            .NotEmpty();

            RuleFor(i=> i.Password)
            .NotEmpty();
        }
    }
}
