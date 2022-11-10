﻿namespace Mimbly.Api.Controllers;

using FollowUp.Api.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Mimbly.Application.Commands.CompanyContact.CreateCompanyContact;
using Mimbly.Application.Commands.CompanyContact.DeleteCompanyContact;
using Mimbly.Application.Commands.CompanyContact.UpdateCompanyContact;
using Mimbly.Application.Contracts.Dtos.CompanyContact;
using Mimbly.Application.Queries.CompanyContact.GetAll;
using Mimbly.Application.Queries.CompanyContact.GetById;

[ApiController]
//[Authorize]
[Route("api/v1/[controller]")]
public class CompanyContactController : BaseController
{
    public CompanyContactController(IMediator mediator) : base(mediator)
    {
    }

    [HttpGet]
    public async Task<ActionResult<AllCompanyContactsVm>> GetAllCompanyContacts()
    {
        return Ok(await _mediator.Send(new GetAllCompanyContactsQuery { }));
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<CompanyContactByIdVm>> FilterCompaniesById([BindRequired] Guid id)
    {
        return Ok(await _mediator.Send(new GetByIdCompanyContactQuery { Id = id }));
    }

    [HttpPost]
    public async Task<ActionResult> CreateCompanyContact([FromBody] CreateCompanyContactRequestDto createCompanyContactRequestDto)
    {
        await _mediator.Send(new CreateCompanyContactCommand { CreateCompanyContactRequest = createCompanyContactRequestDto });

        return Ok("Company contact created successfully");
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> DeleteCompanyContact([BindRequired] Guid id)
    {
        await _mediator.Send(new DeleteCompanyContactCommand { Id = id });

        return Ok("Company contact removed successfully");
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult> UpdateCompanyContact(Guid id, [FromBody] UpdateCompanyContactRequestDto updateCompanyContactRequestDto)
    {
        await _mediator.Send(new UpdateCompanyContactCommand { UpdateCompanyContactRequest = updateCompanyContactRequestDto, Id = id });

        return Ok("Company contact updated successfully");
    }
}