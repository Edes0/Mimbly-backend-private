namespace Mimbly.Api.Controllers.v1;

using Application.Commands.AD.AddCompanyToAd;
using Application.Commands.AD.RemoveCompanyFromAd;
using Application.Contracts.Dtos.AD;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Mimbly.Application.Commands.Company.CreateCompany;
using Mimbly.Application.Commands.Company.DeleteCompany;
using Mimbly.Application.Commands.Company.UpdateCompany;
using Mimbly.Application.Contracts.Dtos.Company;
using Mimbly.Application.Queries.Company.GetAll;
using Mimbly.Application.Queries.Company.GetById;
using Mimbly.Application.Queries.Company.GetCompanyWithChildrenById;

[ApiController]
[Authorize]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public class CompanyController : BaseController
{
    public CompanyController(IMediator mediator) : base(mediator)
    {
    }

    [HttpGet]
    public async Task<ActionResult<AllCompaniesVm>> GetAllCompanies()
    {
        return Ok(await _mediator.Send(new GetAllCompaniesQuery { }));
    }

    [HttpGet("{id:guid}", Name = "CompanyById")]
    public async Task<ActionResult<CompanyByIdVm>> FilterCompaniesById([BindRequired] Guid id)
    {
        return Ok(await _mediator.Send(new GetByIdCompanyQuery { Id = id }));
    }

    [HttpGet("WithChildren/{id:guid}")]
    public async Task<ActionResult<CompanyByIdVm>> CompanyWithChildrenById([BindRequired] Guid id)
    {
        return Ok(await _mediator.Send(new GetCompanyWithChildrenByIdQuery { Id = id }));
    }

    [HttpPost]
    public async Task<ActionResult> CreateCompany([FromBody] AddCompanyRequestDto addCompanyRequestDto)
    {
        var groupId = await _mediator.Send(new AddCompanyToAdCommand { AddCompanyRequestToAdRequest = addCompanyRequestDto });

        if (!Guid.TryParse(groupId, out var groupGuid))
        {
            return StatusCode(500);
        }

        var createdCompany = await _mediator.Send(new CreateCompanyCommand { CreateCompanyRequest = new CreateCompanyRequestDto { Name = addCompanyRequestDto.Name, Id = groupGuid, ParentId = addCompanyRequestDto.ParentId } });

        return new CreatedAtRouteResult("CompanyById", new { id = createdCompany.Id }, createdCompany);

    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> DeleteCompany([BindRequired] Guid id)
    {
        await _mediator.Send(new DeleteCompanyCommand { Id = id });
        await _mediator.Send(new RemoveCompanyFromAdCommand { Id = id });

        return NoContent();
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult> UpdateCompany(Guid id, [FromBody] UpdateCompanyRequestDto updateCompanyRequestDto)
    {
        await _mediator.Send(new UpdateCompanyCommand { UpdateCompanyRequest = updateCompanyRequestDto, Id = id });

        return Ok("Company updated successfully");
    }
}