using KanbanBackend.Models;
using KanbanBackend.Services;
using Microsoft.AspNetCore.Mvc;

namespace KanbanBackend.Controllers;

[ApiController]
[Route("[controller]")]
public class BoardController : Controller
{
    private readonly ILogger<BoardController> _logger;

    private readonly IBoardService _boardService;

    public BoardController(ILogger<BoardController> logger, IBoardService boardService)
    {
        _logger = logger;
        _boardService = boardService;
    }

    [HttpGet]
    public async Task<IEnumerable<Board>> Index()
    {
        var boards = await _boardService.GetAllBoards();
        return boards;
    }
}