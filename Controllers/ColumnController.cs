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

    [HttpGet("board/{boardId}/column/{ColumnId}")]
    public ActionResult<IEnumerable<Column>> GetColumnsOfBoard(int boardId, int ColumnId)
    {
        return Ok();
    }
}