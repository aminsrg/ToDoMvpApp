using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDoMvpApp.Application.Commands.ToDo.Create;
using ToDoMvpApp.Application.Commands.ToDo.Delete;
using ToDoMvpApp.Application.Commands.ToDo.Update;
using ToDoMvpApp.Application.Queries.GetToDoById;
using ToDoMvpApp.Application.Queries.GetToDos;
using ToDoMvpApp.Application.Queries.GetToDosByDate;

namespace ToDoMvpApp.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ToDoController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> Create([FromBody] CreateTodoCommand command)
    {
        var id = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id }, new { id });
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll([FromQuery] GetToDosQuery query)
    {
        var todos = await _mediator.Send(query);
        return Ok(todos);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(string id)
    {
        var todo = await _mediator.Send(new GetToDoByIdQuery(id));
        if (todo is null) return NotFound();
        return Ok(todo);
    }

    [HttpGet("by-date")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByDate([FromQuery] DateTime date)
    {
        var todos = await _mediator.Send(new GetToDosByDateQuery(date));
        return Ok(todos);
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Update([FromBody] UpdateToDoCommand command)
    {
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Delete(string id)
    {
        await _mediator.Send(new DeleteToDoCommand(id));
        return NoContent();
    }
}
