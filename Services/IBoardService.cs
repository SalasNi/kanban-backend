using KanbanBackend.Dtos;
using KanbanBackend.Models;

namespace KanbanBackend.Services;

public interface IBoardService
{
    public Task<IEnumerable<Board>> GetAllBoards();
    public Task<Board?> FindById(int id);
    public Task Remove(int id);
    public Task<Board> Create(CreateBoardDto createBoardDto);
}