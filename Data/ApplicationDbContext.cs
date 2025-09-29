using KanbanBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace KanbanBackend.Data;


public class ApplicationDbContext : DbContext
{
    public required DbSet<Board> Boards { get; set; }
    public required DbSet<Column> Columns { get; set; }
    public required DbSet<Issue> Issues { get; set; }
    public required DbSet<Tag> Tags { get; set; }

    public ApplicationDbContext(DbContextOptions options) : base(options) { }
}