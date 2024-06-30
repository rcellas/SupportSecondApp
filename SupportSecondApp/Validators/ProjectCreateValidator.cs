using FluentValidation;
using SupportSecondApp.DTOs;

namespace SupportSecondApp.Validators;

public class ProjectCreateValidator : AbstractValidator<ProjectCreateDto>
{
    public ProjectCreateValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage(FrequenceMessage.EmptyMessage)
            .MaximumLength(50).WithMessage("El máximo texto para Name es de 50 caracteres").NotNull().WithMessage(FrequenceMessage.EmptyMessage);
    }
}