namespace Mimbly.Api.Controllers;

using FollowUp.Api.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Mimbly.Application.Queries.Company.GetAll;
using Mimbly.Application.Queries.Company.GetById;
using Mimbly.Application.Queries.Company.GetCompanyWithChildrenById;

[ApiController]
//[Authorize] //TODO: LIsta ur hur man anv�nder authorization
[Route("api/v1/[controller]")]
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

    [Route("ById")]
    [HttpGet]
    public async Task<ActionResult<CompanyByIdVm>> FilterCompaniesById([BindRequired, FromQuery] Guid id)
    {
        return Ok(await _mediator.Send(new GetByIdCompanyQuery { Id = id }));
    }

    [Route("WithChildren")]
    [HttpGet]
    public async Task<ActionResult<CompanyByIdVm>> CompanyWithChildrenById([BindRequired, FromQuery] Guid id)
    {
        return Ok(await _mediator.Send(new GetCompanyWithChildrenByIdQuery { Id = id }));
    }

    //[HttpPost]
    //public async Task<ActionResult> CreateCompany([FromBody] CreateCompanyRequestDto createCompanyRequestDto)
    //{
    //    await _mediator.Send(new CreateCompanyCommand { CreateCompanyRequest = createCompanyRequestDto });

    //    return Ok("Company created successfully");
    //}

    //    [HttpDelete]
    //    public async Task<ActionResult> DeleteCompany([BindRequired, FromQuery] Guid id)
    //    {
    //        await _mediator.Send(new DeleteCompanyCommand { Id = id });

    //        return Ok("Company removed successfully");
    //}
}