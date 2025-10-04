using KanbanBackend.Dtos;
using KanbanBackend.Exceptions;
using KanbanBackend.Models;
using KanbanBackend.Services;
using Microsoft.AspNetCore.Mvc;

namespace KanbanBackend.Controllers;

[ApiController]
public class ColumnController : Controller
{
    private readonly ILogger<ColumnController> _logger;

    private readonly IColumnService _columnService;

    public ColumnController(ILogger<ColumnController> logger, IColumnService columnService)
    {
        _logger = logger;
        _columnService = columnService;
    }

    [HttpGet("board/{boardId}/column")]
    public async Task<ActionResult<IEnumerable<Column>>> GetColumnsOfBoard(int boardId)
    {
        var data = await _columnService.GetColumnsOfBoard(boardId);
        return Ok(data);
    }

    [HttpPut("{ColumnId}")]
    public async Task<ActionResult> UpdateColumn(int ColumnId, UpdateColumnDto updateColumnDto)
    {
        try
        {
            await _columnService.Update(ColumnId, updateColumnDto);
            return Ok();
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (Exception)
        {
            return Problem("Unexpected error");
        }
        
    }

}