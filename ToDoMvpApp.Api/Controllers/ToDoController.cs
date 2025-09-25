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
    public async Task<IActionResult> Create([FromBody] CreateTodoCommand command)
    {
        var id = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id }, new { id });
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] GetToDosQuery query)
    {
        var res = await _mediator.Send(query);
        return Ok(res);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        var res = await _mediator.Send(new GetToDoByIdQuery(id));
        if (res is null) return NotFound();
        return Ok(res);
    }

    [HttpGet("by-date")]
    public async Task<IActionResult> GetByDate([FromQuery] DateTime date)
    {
        var res = await _mediator.Send(new GetToDosByDateQuery(date));
        return Ok(res);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, [FromBody] UpdateToDoCommand command)
    {
        if (id != command.Id) return BadRequest();
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        await _mediator.Send(new DeleteToDoCommand(id));
        return NoContent();
    }
}
