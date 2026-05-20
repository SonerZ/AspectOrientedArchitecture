using FluentValidation;
using FluentValidation.Results;
using System.Threading.Tasks;

namespace Application.Packages.AOP.Aspects.Validation;

public static class ValdationTool
{
    public async static Task<ValidationResult> Validate(IValidator validator, object entity)
    {
        var context = new ValidationContext<object>(entity);

       return await validator.ValidateAsync(context);
    }
}