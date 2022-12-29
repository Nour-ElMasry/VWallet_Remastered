using Application.Commands;
using Application.Queries;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VWallet_API.Dtos.CryptoDtos;
using VWallet_API.Dtos.TransactionDtos;

namespace VWallet_API.Controllers;

[Route("api/v1/Users")]
[ApiController]
public class CryptoController : ControllerBase
{
    public readonly IMapper _mapper;
    public readonly IMediator _mediator;
    public readonly ILogger _logger;

    public CryptoController(IMapper mapper, IMediator mediator, ILogger<object> logger)
    {
        _mapper = mapper;
        _mediator = mediator;
        _logger = logger;
        _logger.LogInformation("Crypto Controller called...");
    }

    [HttpGet]
    [Route("Crypto/All")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetAllCryptos()
    {
        _logger.LogInformation($"Preparing to get all cryptos...");

        var result = await _mediator.Send(new GetAllCryptoCurrenties());

        if (result == null)
        {
            _logger.LogError("Couldn't get cryptos!!!");
            return NotFound();
        }

        _logger.LogInformation("All cryptos received successfully!!!");

        return Ok(result);
    }


    [HttpGet]
    [Route("{id}/Crypto/All")]
    [Authorize]
    public async Task<IActionResult> GetAllUserCryptos(string id)
    {
        _logger.LogInformation($"Preparing to get all cryptos of user with id {id}...");

        var result = await _mediator.Send(new GetCryptoByUserId
        {
            UserId = id
        });

        if (result == null)
        {
            _logger.LogError($"Couldn't get cryptos of user with id {id}!!!");
            return NotFound();
        }

        _logger.LogInformation($"All cryptos of user with id {id} received successfully!!!");

        return Ok(result);
    }


    [HttpPost]
    [Route("{id}/Crypto/Investment")]
    [Authorize]
    public async Task<IActionResult> MakeInvestment(string id, [FromBody] CryptoPostDto inv)
    {
        _logger.LogInformation($"Preparing to make investment...");

        var result = await _mediator.Send(new CreateCryptoInvestment
        {
            UserId = id,
            Name = inv.Name,
            Investment = inv.Investment,
            Value = inv.Value
        });

        if (result == null)
        {
            _logger.LogError("Couldn't make investment!!!");
            return NotFound();
        }

        _logger.LogInformation("Investment made successfully!!!");

        return Ok();
    }

    [HttpDelete]
    [Route("{id}/Crypto/{ccId}/Cash-out/{value}")]
    [Authorize]
    public async Task<IActionResult> CashOutInvestment(string id, long ccId, long value)
    {
        _logger.LogInformation($"Preparing to cash out investment...");

        var result = await _mediator.Send(new CashOutCrypto
        {
            UserId = id,
            CryptoId = ccId,
            CurrentValue = value
        });

        if (result == null)
        {
            _logger.LogError("Couldn't cash out investment!!!");
            return NotFound();
        }

        _logger.LogInformation("cash out investment made successfully!!!");

        return Ok();
    }
}
