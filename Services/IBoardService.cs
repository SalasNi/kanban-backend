using KanbanBackend.Models;

namespace KanbanBackend.Services;

public interface IBoardService
{
    public Task<IEnumerable<Board>> GetAllBoards();
}