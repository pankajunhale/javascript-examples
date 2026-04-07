using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TodoList.Api.Application.Common;
using TodoList.Api.Application.DTOs.Todos;
using TodoList.Api.Application.Exceptions;
using TodoList.Api.Application.Interfaces.Services;

namespace TodoList.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public sealed class TodosController(ITodoService todoService) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<PaginatedResult<TodoResponse>>> GetTodos([FromQuery] PaginationQuery query, CancellationToken cancellationToken)
    {
        var userId = GetUserId();
        var result = await todoService.GetPagedForUserAsync(userId, query, cancellationToken);
        return Ok(result);
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TodoResponse>> GetTodoById(int id, CancellationToken cancellationToken)
    {
        var userId = GetUserId();
        var todo = await todoService.GetByIdAsync(id, userId, cancellationToken);
        return Ok(todo);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<TodoResponse>> CreateTodo([FromBody] CreateTodoRequest request, CancellationToken cancellationToken)
    {
        var userId = GetUserId();
        var created = await todoService.CreateAsync(request, userId, cancellationToken);
        return CreatedAtAction(nameof(GetTodoById), new { id = created.Id }, created);
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TodoResponse>> UpdateTodo(
        int id,
        [FromBody] UpdateTodoRequest request,
        CancellationToken cancellationToken)
    {
        var userId = GetUserId();
        var updated = await todoService.UpdateAsync(id, request, userId, cancellationToken);
        return Ok(updated);
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteTodo(int id, CancellationToken cancellationToken)
    {
        var userId = GetUserId();
        await todoService.DeleteAsync(id, userId, cancellationToken);
        return NoContent();
    }

    private int GetUserId()
    {
        var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
        return int.TryParse(userIdClaim, out var userId) ? userId : throw new UnauthorizedException("Invalid token.");
    }
}
