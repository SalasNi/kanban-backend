using KanbanBackend.Data;
using KanbanBackend.Dtos;
using KanbanBackend.Exceptions;
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
        var boards = await _dbContext.Boards
        .Where(i =>
            i.DeletedAt == null
        )
        .ToListAsync();

        return boards;
    }

    public async Task<Board?> FindById(int id)
    {
        var board = await _dbContext.Boards
            .Where(b =>
                b.DeletedAt == null &&
                b.Id == id
            )
            .FirstOrDefaultAsync();

        return board;
    }

    public async Task Remove(int id)
    {
        var board = await FindById(id);

        if (board == null)
        {
            throw new NotFoundException($"Board with ID: {id}: Not Found");
        }

        board.DeletedAt = DateTime.UtcNow;

        await _dbContext.SaveChangesAsync();
    }

    public async Task<Board> Create(CreateBoardDto createBoardDto)
    {
        var newBoard = new Board
        {
            Name = createBoardDto.Name,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        await using var transaction = await _dbContext.Database.BeginTransactionAsync();
        try
        {
            await _dbContext.Boards.AddAsync(newBoard);
            await _dbContext.SaveChangesAsync();

            return newBoard;
        }
        catch (Exception)
        {
            throw;
        }
    }
}