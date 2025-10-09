using Bogus;
using KanbanBackend.Models;

namespace KanbanBackend.Data;


public class Dataseed
{
    ApplicationDbContext _dbContext;

    public Dataseed(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }


    public async Task Seed()
    {
        Console.WriteLine("Starting seeding data");
        using var transaction = await _dbContext.Database.BeginTransactionAsync();

        try
        {
            for (int i = 0; i < 10; i++)
            {
                var testBoard = new Faker<Board>().Rules((f, o) =>
                {
                    o.CreatedAt = DateTime.UtcNow;
                    o.UpdatedAt = DateTime.UtcNow;
                    o.Name = f.Commerce.Product();
                });

                var newBoard = testBoard.Generate();
                _dbContext.Boards.Add(newBoard);

                await _dbContext.SaveChangesAsync();

                var todoColumn = new Column()
                {
                    Name = "To Do",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    Board = newBoard
                };

                await _dbContext.Columns.AddAsync(todoColumn);

                var doneColumn = new Column()
                {
                    Name = "Done",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    Board = newBoard
                };

                await _dbContext.Columns.AddAsync(doneColumn);

                var progressColumn = new Column()
                {
                    Name = "In Progress",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    Board = newBoard
                };

                await _dbContext.Columns.AddAsync(progressColumn);
                await _dbContext.SaveChangesAsync();

                var testIssue = new Faker<Issue>().Rules((f, o) =>
                {
                    o.Title = f.Commerce.Product();
                    o.Description = f.Lorem.Sentence();
                    o.CreatedAt = DateTime.UtcNow;
                    o.UpdatedAt = DateTime.UtcNow;
                    o.Board = testBoard;
                    o.Column = f.PickRandom(todoColumn, todoColumn, progressColumn);
                });

                var randomAmount = new Random();
                var issuesList = testIssue.Generate(randomAmount.Next(10, 50));

                _dbContext.Issues.AddRange(issuesList);
                await _dbContext.SaveChangesAsync();
            }

            await transaction.CommitAsync();
            Console.WriteLine("Seed succesfull");
        }
        catch (Exception)
        {
            await transaction.RollbackAsync();
            throw;
        }
    }
}