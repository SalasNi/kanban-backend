using System.ComponentModel.DataAnnotations;

namespace KanbanBackend.Dtos;

public class CreateBoardDto
{
    [Required]
    [MaxLength(250)]
    public required string Name { get; set; }
}