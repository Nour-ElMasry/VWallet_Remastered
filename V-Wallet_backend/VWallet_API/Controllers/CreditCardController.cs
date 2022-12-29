using Application.Commands;
using Application.Queries;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VWallet_API.Dtos.CreditCardDtos;
using VWallet_API.Dtos.TransactionDtos;

namespace VWallet_API.Controllers;

[Route("api/v1/Users")]
[ApiController]
public class CreditCardController : ControllerBase
{
    public readonly IMapper _mapper;
    public readonly IMediator _mediator;
    public readonly ILogger _logger;

    public CreditCardController(IMapper mapper, IMediator mediator, ILogger<object> logger)
    {
        _mapper = mapper;
        _mediator = mediator;
        _logger = logger;
        _logger.LogInformation("CreditCard Controller called...");
    }

    [HttpGet]
    [Route("CreditCards/All")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetAllCreditCards()
    {
        _logger.LogInformation($"Preparing to get all credit cards...");

        var result = await _mediator.Send(new GetAllCreditCards());

        if (result == null)
        {
            _logger.LogError("Couldn't get credit cards!!!");
            return NotFound();
        }

        var mappedResult = _mapper.Map<List<CreditCardInfoGetDto>>(result);

        _logger.LogInformation("All credit cards received successfully!!!");

        return Ok(mappedResult);
    }

    [HttpGet]
    [Route("/CreditCards/CheckIban/{iban}")]
    public async Task<IActionResult> CheckIbanExistance(string iban)
    {
        _logger.LogInformation($"Preparing to check if IBAN:{iban} exists...");

        var result = await _mediator.Send(new CheckIbanExistance
        {
            Iban = iban
        });

        if (result)
        {
            _logger.LogInformation($"IBAN:{iban} exists!!!");
        }
        else
        {
            _logger.LogInformation($"IBAN:{iban} doesn't exists!!!");
        }

        return Ok(result);
    }

    [HttpGet]
    [Route("{id}/CreditCards/All")]
    [Authorize]
    public async Task<IActionResult> GetAllUserCreditCards(string id)
    {
        _logger.LogInformation($"Preparing to get credit cards of user with id {id}...");

        var result = await _mediator.Send(new GetCreditCardsByUserId { UserId = id });

        if (result == null)
        {
            _logger.LogError("Couldn't get user's credit cards!!!");
            return NotFound();
        }

        var mappedResult = _mapper.Map<List<CreditCardInfoGetDto>>(result);

        _logger.LogInformation("All user's credit cards received successfully!!!");

        return Ok(mappedResult);
    }

    [HttpGet]
    [Route("{id}/CreditCards/{ccId}")]
    [Authorize]
    public async Task<IActionResult> GetCreditCardById(string id, long ccId)
    {
        _logger.LogInformation($"Preparing to get credit card with id {ccId} of user with id {id}...");

        var result = await _mediator.Send(new GetCreditCardById { 
            UserId = id,
            CreditCardId = ccId
        });

        if (result == null)
        {
            _logger.LogError("Couldn't get credit card!!!");
            return NotFound();
        }

        var mappedResult = _mapper.Map<CreditCardGetDto>(result);

        _logger.LogInformation($"Credit card with id {id} received successfully!!!");

        return Ok(mappedResult);
    }

    [HttpPost]
    [Route("CreditCards/Transaction")]
    [Authorize]
    public async Task<IActionResult> MakeTransaction([FromBody] TransactionPostDto trans)
    {
        _logger.LogInformation($"Preparing to make transaction...");

        var result = await _mediator.Send(new CreateTransaction
        {
            SendingCCIban = trans.SendingCCIban,
            ReceivingCCIban = trans.ReceivingCCIban,
            Amount = trans.Amount
        });

        if (result == null)
        {
            _logger.LogError("Couldn't make transaction!!!");
            return NotFound();
        }

        var mappedResult = _mapper.Map<TransactionGetDto>(result);

        _logger.LogInformation("Transaction made successfully!!!");

        return Ok(mappedResult);
    }

    [HttpDelete]
    [Route("CreditCards/{ccId}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteCreditCard(long ccId)
    {
        _logger.LogInformation($"Preparing to delete CreditCard with id {ccId}...");

        var command = new DeleteCreditCard { CreditCardId = ccId };
        var result = await _mediator.Send(command);

        if (result == null)
        {
            _logger.LogError($"CreditCard with id {ccId} not found!!!");
            return NotFound();
        }

        _logger.LogInformation($"CreditCard with id {ccId} deleted successfully!!!");

        return NoContent();
    }

    [HttpDelete]
    [Route("{Id}/CreditCards/{ccId}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteUserCreditCard(string id, long ccId)
    {
        _logger.LogInformation($"Preparing to delete CreditCard with id {ccId} of User with id {id}...");

        var command = new DeleteUserCreditCard 
        {
            UserId = id,
            CreditCardId = ccId
        };

        var result = await _mediator.Send(command);

        if (result == null)
        {
            _logger.LogError($"CreditCard with id {ccId} of User with id {id} not found!!!");
            return NotFound();
        }

        _logger.LogInformation($"CreditCard with id {ccId} of User with id {id} deleted successfully!!!");

        return NoContent();
    }
}
