namespace KanbanBackend.Models;

public class Tag
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public ICollection<Issue>? Items { get; set; }
}