﻿namespace Mimbly.Application.Common.Validators.MimboxErrorLog;

using FluentValidation;
using Mimbly.Application.Contracts.Dtos.MimboxErrorLog;

public class UpdateMimboxErrorLogRequestDtoValidator : AbstractValidator<UpdateMimboxErrorLogRequestDto>
{
    public UpdateMimboxErrorLogRequestDtoValidator()
    {
        RuleFor(x => x.Discarded)
                .NotNull().WithMessage("Discarded is required");
    }
}