using MyDevLog.API.DTOs;

namespace MyDevLog.API.Services;


public interface IGitHubService
{
    Task<IEnumerable<GitHubRepositoryDTO>> GetPinnedRepositoriesAsync();
}