namespace KanbanBackend.Models;

public class Issue
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public string? Description { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
    public required Board Board { get; set; }
    public Column? Column { get; set; }
    public ICollection<Tag>? Tags { get; set; }
}