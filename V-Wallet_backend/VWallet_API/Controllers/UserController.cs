using Application.Queries;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VWallet_API.Dtos;

namespace VWallet_API.Controllers;

[Route("api/v1/User")]
[ApiController]
public class UserController : ControllerBase
{
    public readonly IMapper _mapper;
    public readonly IMediator _mediator;
    public readonly ILogger _logger;

    public UserController(IMapper mapper, IMediator mediator, ILogger<object> logger)
    {
        _mapper = mapper;
        _mediator = mediator;
        _logger = logger;
        _logger.LogInformation("User Controller is called...");
    }

    [HttpGet]
    [Route("All/{pg?}")]
    public async Task<IActionResult> GetAllUsers(int pg = 1)
    {
        _logger.LogInformation("Preparing to get all users...");

        var result = await _mediator.Send(new GetAllUsers());

        if (result == null)
        {
            _logger.LogError("Couldn't get all users!!!");
            return NotFound();
        }

        var mappedResult = _mapper.Map<List<UserGetDto>>(result);

        _logger.LogInformation("All users received successfully!!!");

        return Ok(mappedResult);
    }
}
