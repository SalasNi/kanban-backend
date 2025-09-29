namespace KanbanBackend.Models;

public class Issue
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public required string Description { get; set; }
    public DateTime? DeletedAt { get; set; }
    public ICollection<Tag>? Tags { get; set; }
}