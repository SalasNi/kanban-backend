using System.ComponentModel.DataAnnotations;

namespace KanbanBackend.Dtos;

public class CreateIssueDto
{
    [Required]
    public required string Title { get; set; }
    public string? Description { get; set; }

    [Required]
    public int BoardId { get; set; }
}