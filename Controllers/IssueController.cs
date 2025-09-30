using KanbanBackend.Dtos;
using KanbanBackend.Exceptions;
using KanbanBackend.Services;
using Microsoft.AspNetCore.Mvc;

namespace KanbanBackend.Controllers;


[ApiController]
[Route("[controller]")]
public class IssueController : Controller
{
    private readonly ILogger<IssueController> _logger;

    private readonly IIssueService _issueService;

    public IssueController(ILogger<IssueController> logger, IIssueService issueService)
    {
        _logger = logger;
        _issueService = issueService;
    }


    [HttpPost]
    public async Task<IActionResult> Create(CreateIssueDto createIssueDto)
    {
        try
        {
            var newIssue = await _issueService.Create(createIssueDto);
            return Ok(newIssue);
        }
        catch
        {
            return Problem("Unexpected Error");
        }
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> Remove(int id)
    {
        try
        {
            await _issueService.Remove(id);
            return Ok();
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (Exception)
        {
            return Problem("Unexpected error");
        }
    }
}