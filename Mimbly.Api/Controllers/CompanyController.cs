namespace Mimbly.Api.Controllers;

using FollowUp.Api.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Mimbly.Application.Queries.Company.GetAll;
using Mimbly.Application.Queries.Company.GetById;

[ApiController]
//[Authorize] //TODO: LIsta ur hur man anv�nder authorization
[Route("api/v1/[controller]")]
public class CompanyController : BaseController
{
    public CompanyController(IMediator mediator) : base(mediator)
    {
    }

    [HttpGet]
    public async Task<ActionResult<CompaniesNotFiltered>> GetAllComapnies()
    {
        return Ok(await _mediator.Send(new GetAllCompaniesQuery { }));
    }

    [Route("ById")]
    [HttpGet]
    public async Task<ActionResult<CompanyFilteredById>> FilterComapniesById([BindRequired, FromQuery] Guid id)
    {
        return Ok(await _mediator.Send(new GetFilterByIdCompanyQuery { Id = id }));
    }

    //    [HttpPost]
    //    public async Task<ActionResult> CreateCompany([FromBody] CreateCompanyRequestDto createCompanyRequestDto)
    //    {
    //        await _mediator.Send(new CreateCompanyCommand { CreateCompanyRequest = createCompanyRequestDto });

    //        return Ok("Company created successfully");
    //    }

    //    [HttpDelete]
    //    public async Task<ActionResult> DeleteCompany([BindRequired, FromQuery] Guid id)
    //    {
    //        await _mediator.Send(new DeleteCompanyCommand { Id = id });

    //        return Ok("Company removed successfully");
    //}
}