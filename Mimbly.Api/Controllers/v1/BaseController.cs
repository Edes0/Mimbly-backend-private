namespace Mimbly.Api.Controllers.v1;

using System;
using MediatR;
using Microsoft.AspNetCore.Mvc;

[ApiController]
public abstract class BaseController : ControllerBase
{
    internal readonly IMediator _mediator;

    protected BaseController(IMediator mediator) => _mediator = mediator;

    /// <summary>
    /// Returns the bearer token from the Request.
    /// </summary>
    /// <returns>Token.</returns>
    protected string CurrentToken
    {
        get
        {
            string authorization = Request.Headers["Authorization"];
            return authorization.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase) ? authorization["Bearer ".Length..].Trim() : string.Empty;
        }
    }
}