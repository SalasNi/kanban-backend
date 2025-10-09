using KanbanBackend.Data;
using KanbanBackend.Dtos;
using KanbanBackend.Exceptions;
using KanbanBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace KanbanBackend.Services;

public class ColumnService : IColumnService
{
    private readonly ILogger<ColumnService> _logger;

    private readonly ApplicationDbContext _dbContext;

    public ColumnService(ILogger<ColumnService> logger, ApplicationDbContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Column>> GetColumnsOfBoard(int boardId)
    {
        var columns = await _dbContext.Columns
        .Where(c =>
            c.DeletedAt == null &&
            c.Board.Id == boardId
        )
        .Include(c =>
            c.Issues.Where(
                i => i.DeletedAt == null
            )
        )
        .ToListAsync();

        return columns;
    }

    public async Task<Column> FindById(int id)
    {
        var column = await _dbContext.Columns
        .Where(c =>
            c.Id == id &&
            c.DeletedAt == null
        )
        .FirstOrDefaultAsync();

        if (column == null)
        {
            throw new NotFoundException($"Column: ${id} not found");
        }

        return column;
    }

    public async Task Update(int id, UpdateColumnDto updateColumnDto)
    {
        var column = await FindById(id);

        if (updateColumnDto.Name != null)
            column.Name = updateColumnDto.Name;

        await _dbContext.SaveChangesAsync();
    }
}