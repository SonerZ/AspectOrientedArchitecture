using System;
using DefaultNamespace;
using FluentValidation;

namespace Application.Business.ValidationRules.FluentValidation;

public class BaseRequestDtoValidator: AbstractValidator<BaseIdDTO>
{
    public BaseRequestDtoValidator()
    {
        RuleFor(i => i.Data)
            .NotNull()
            .Must(NotDefault);
    }

    bool NotDefault(string data) => data != string.Empty;
}