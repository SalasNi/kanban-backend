using KanbanBackend.Dtos;
using KanbanBackend.Models;

namespace KanbanBackend.Services;

public interface IColumnService
{
    public Task<IEnumerable<Column>> GetColumnsOfBoard(int boardId);
    public Task<Column> FindById(int id);
    public Task Update(int id, UpdateColumnDto updateColumnDto);
}