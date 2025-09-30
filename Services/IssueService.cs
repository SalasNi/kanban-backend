using KanbanBackend.Data;
using KanbanBackend.Dtos;
using KanbanBackend.Exceptions;
using KanbanBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace KanbanBackend.Services;

public class IssueService : IIssueService
{
    private readonly ILogger<IssueService> _logger;

    private readonly ApplicationDbContext _dbContext;

    public IssueService(ILogger<IssueService> logger, ApplicationDbContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }

    public async Task<Issue> Create(CreateIssueDto createIssueDto)
    {
        var board = await _dbContext.Boards.Where(b =>
            b.Id == createIssueDto.BoardId &&
            b.DeletedAt == null
        ).FirstOrDefaultAsync();

        if (board == null)
        {
            throw new NotFoundException($"Board with ID: {createIssueDto.BoardId} not found");
        }

        var newIssue = new Issue
        {
            Title = createIssueDto.Title,
            Description = createIssueDto.Description,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            Board = board
        };

        await using var transaction = await _dbContext.Database.BeginTransactionAsync();
        await _dbContext.Issues.AddAsync(newIssue);
        await _dbContext.SaveChangesAsync();

        return newIssue;
    }

    public async Task<Issue> FindById(int id)
    {
        var issue = await _dbContext.Issues
            .Where(i =>
                i.Id == id &&
                i.DeletedAt == null
            )
            .FirstOrDefaultAsync();

        if (issue == null)
        {
            throw new NotFoundException($"Issue with ID: {id} not found");
        }

        return issue;
    }

    public async Task<IEnumerable<Issue>> GetAll()
    {
        var issues = await _dbContext.Issues
            .Where(i =>
                i.DeletedAt == null
            )
            .ToListAsync();

        return issues;
    }

    public async Task Remove(int id)
    {
        var issue = await FindById(id);
        issue.DeletedAt = DateTime.UtcNow;

        await _dbContext.SaveChangesAsync();
    }
}