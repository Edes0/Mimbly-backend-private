namespace Mimbly.Application.Contracts.Dtos.AD;

using CoreServices.Validation;
using FluentValidation;

public class AddCompanyDto
{

    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public Guid ParentId { get; set; }

    public async Task Validate()
    {
        await ValidatableEntity.ValidateEntityByFluentRules(this, new CreateCompanyValidator());
    }
}

internal class CreateCompanyValidator : AbstractValidator<AddCompanyDto>
{
    public CreateCompanyValidator()
    {
        RuleFor(company => company.Name).NotEmpty();
        RuleFor(company => company.Description).NotEmpty();
        RuleFor(company => company.ParentId).NotNull().NotEmpty();
    }
}