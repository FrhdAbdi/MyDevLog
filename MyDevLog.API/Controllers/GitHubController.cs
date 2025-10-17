using Microsoft.AspNetCore.Mvc;
using MyDevLog.API.DTOs;
using MyDevLog.API.Services;

namespace MyDevLog.API.Controllers;


[ApiController]
[Route("api/[controller]")]
public class GitHubController : ControllerBase
{
    private readonly IGitHubService _githubService;
    public GitHubController(IGitHubService githubService)
    {
        _githubService = githubService;
    }


    [HttpGet("pinned-repositories")]
    [ResponseCache(Duration = 3600)]
    public async Task<ActionResult<IEnumerable<GitHubRepositoryDTO>>> GetPinnedRepositories()
    {
        try
        {
            var repositories = await _githubService.GetPinnedRepositoriesAsync();
            return Ok(repositories);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An internal server error occurred: {ex.Message}");
        }
    }
}