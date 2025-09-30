using KanbanBackend.Dtos;
using KanbanBackend.Models;

namespace KanbanBackend.Services;

public interface IIssueService
{
    public Task<IEnumerable<Issue>> GetAll();
    public Task<Issue> FindById(int id);
    public Task<Issue> Create(CreateIssueDto createIssueDto);
    public Task Remove(int id);
}