using Application.Commands;
using Application.Dto;
using Application.Queries;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VWallet_API.Dtos.UserDtos;

namespace VWallet_API.Controllers;

[Route("api/v1/Users")]
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
    [Route("All")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetAllUsers()
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

    [HttpGet]
    [Route("unique/{username}")]
    public async Task<IActionResult> CheckUnique(string username)
    {
        _logger.LogInformation("Preparing to check if username is unique...");

        var result = await _mediator.Send(new CheckUniqueUsername
        {
            Username = username
        });

        if (result)
        {
            _logger.LogInformation("Username is unique!!!");
        }
        else
        {
            _logger.LogInformation("Username is not unique!!!");
        }

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] UserPostDto user)
    {
        _logger.LogInformation("Preparing to create a user...");

        if (!ModelState.IsValid)
        {
            _logger.LogError("Information received was invalid!!");
            return BadRequest(ModelState);
        }

        var command = _mapper.Map<CreateUser>(user);

        var result = await _mediator.Send(command);

        if (result == null)
        {
            _logger.LogError($"Failed to create user!!!");
            return NotFound();
        }

        _logger.LogInformation("User created successfully!!!");

        return Ok(result);
    }

    [HttpPut]
    [Route("{id}")]
    [Authorize]
    public async Task<IActionResult> UpdateUser(string id, [FromBody] UserPutDto updated)
    {
        _logger.LogInformation($"Preparing to update user with id {id}...");

        var command = new UpdateUser
        {
            UserId = id,
            Name = updated.Name,
            DateOfBirth = updated.DateOfBirth,
            Country = updated.Country,
            City = updated.City,
            Street = updated.Street
        };

        var result = await _mediator.Send(command);

        if (result == null)
        {
            _logger.LogError($"User with id {id} not found!!!");
            return NotFound();
        }

        _logger.LogInformation($"User with id {id} updated successfully!!!");

        return NoContent();
    }

    [HttpGet]
    [Route("{id}")]
    [Authorize]
    public async Task<IActionResult> GetUserById(string id)
    {
        _logger.LogInformation($"Preparing to get user with id {id}...");

        var command = new GetUserById
        {
            UserId = id
        };

        var result = await _mediator.Send(command);

        if (result == null)
        {
            _logger.LogError($"User with id {id} not found!!!");
            return NotFound();
        }

        var mappedResult = _mapper.Map<UserGetDto>(result);


        _logger.LogInformation($"User with id {id} received successfully!!!");

        return Ok(mappedResult);
    }

    [HttpPost]
    [Route("Auth")]
    public async Task<IActionResult> LoginUser(UserAuthDto user)
    {
        _logger.LogInformation($"Preparing to authenticate user...");

        if (!ModelState.IsValid)
        {
            _logger.LogError("Information received was invalid!!");
            return BadRequest(ModelState);
        }

        var command = new LoginUser
        {
            UserName = user.Username,
            Password = user.Password
        };

        var result = await _mediator.Send(command);

        if (result == null)
        {
            _logger.LogError("Failed to authenticate user!!!");
            return Unauthorized();
        }

        _logger.LogInformation("User authenticated successfully!!!");

        return Ok(result);
    }

    [HttpDelete]
    [Route("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteUser(string id)
    {
        _logger.LogInformation($"Preparing to delete user with id {id}...");

        var command = new DeleteUser { UserId = id };
        var result = await _mediator.Send(command);

        if (result == null)
        {
            _logger.LogError($"User with id {id} not found!!!");
            return NotFound();
        }

        _logger.LogInformation($"User with id {id} deleted successfully!!!");

        return NoContent();
    }
}
