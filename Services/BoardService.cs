using KanbanBackend.Models;

namespace KanbanBackend.Services;

public class BoardService : IBoardService
{
    public Task<IEnumerable<Board>> GetAllBoards()
    {
        throw new NotImplementedException();
    }
}