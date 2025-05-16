using ECommerceApplication.Commands;
using ECommerceDomain;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ECommercePresentation;

[ApiController]
[Route("/")]
public class ECommerceController : ControllerBase
{
    private readonly IMediator _mediator;

    public ECommerceController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPost]
    [Route("create")]
    public async Task<IActionResult> Create(CreateProductCommand command)
    {
        Product result;
        try
        {
            result = await _mediator.Send(command);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
        
        return Ok(result);
    }
    
    [HttpGet]
    [Route("getAll")]
    public async Task<IActionResult> GetAll(Guid? id)
    {
        var command = new SearchProductCommand {Id = id};
        List<Product> result;
        try
        {
            result = await _mediator.Send(command);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
        return Ok(result);
    }
    
    [HttpDelete]
    [Route("delete")]
    public async Task<IActionResult> GetAll(DeleteProductCommand command)
    {
        Guid result;
        try
        {
            result = await _mediator.Send(command);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
        return Ok(result);
    }
}

/*
    middleware
    product create should send an email to a address (..) sandgreed 
    unit tests for command validator
    arch unit for testing the design pattern
*/