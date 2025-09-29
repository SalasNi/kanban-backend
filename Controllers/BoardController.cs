using KanbanBackend.Dtos;
using KanbanBackend.Exceptions;
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

    [HttpPost]
    public async Task<ActionResult<Board>> Create([FromBody] CreateBoardDto createBoardDto)
    {
        try
        {
            var board = await _boardService.Create(createBoardDto);
            return Ok(board);
        }
        catch (Exception e)
        {
            return Problem(e.Message);
        }
    }


    [HttpGet("{id}")]
    public async Task<ActionResult<Board>> Detail(int id)
    {
        try
        {
            var board = await _boardService.FindById(id);
            return Ok(board);
        }
        catch (NotFoundException e)
        {
            return NotFound(e);
        }
        catch (Exception)
        {
            return Problem();
        }
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> Remove(int id)
    {
        try
        {
            await _boardService.Remove(id);
            return Ok();
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (Exception)
        {
            return Problem();
        }
    }
}