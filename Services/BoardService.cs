using KanbanBackend.Data;
using KanbanBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace KanbanBackend.Services;

public class BoardService : IBoardService
{
    private readonly ILogger<BoardService> _logger;

    private readonly ApplicationDbContext _dbContext;

    public BoardService(ILogger<BoardService> logger, ApplicationDbContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Board>> GetAllBoards()
    {
        var boards = await _dbContext.Boards.ToListAsync();
        return boards;
    }
}