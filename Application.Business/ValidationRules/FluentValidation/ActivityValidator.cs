using FluentValidation;
using Application.Entities.CustomEntities.User;
using System;

namespace Application.Business.ValidationRules.FluentValidation
{
    public class ActivityValidator : AbstractValidator<ActivityCreateDto>
    {
        public ActivityValidator()
        {
            RuleFor(i=> i.ActivityType)
            .MinimumLength(5)
            .MaximumLength(60)
            .NotEmpty();

            RuleFor(i=> i.Description)
            .MinimumLength(5)
            .MaximumLength(60)
            .NotEmpty();
           
          RuleFor(i=> i.ActivityDate)
            .NotNull();
        }

        private bool NotDefault(Guid userId) => userId != default;
    }
}
