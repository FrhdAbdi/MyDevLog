using MyDevLog.API.DTOs;
using Octokit;

namespace MyDevLog.API.Services;


public class GitHubService : IGitHubService
{
    private readonly IConfiguration _configuration;
    public GitHubService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<IEnumerable<GitHubRepositoryDTO>> GetPinnedRepositoriesAsync()
    {
        var username = _configuration["GitHub:Username"];
        var pat = _configuration["GitHub:PAT"];

        if (string.IsNullOrEmpty(username))
        {
            throw new InvalidOperationException("GitHub Username is missing in appsettings.");
        }

        var github = new GitHubClient(new ProductHeaderValue("MyPortfolio-API"));
        if (!string.IsNullOrEmpty(pat))
        {
            github.Credentials = new Credentials(pat);
        }

        var allRepositories = await github.Repository.GetAllForUser(username);

        var filteredAndSortedRepos = allRepositories
         .Where(r => !r.Fork && r.Visibility == RepositoryVisibility.Public)
         .OrderByDescending(r => r.UpdatedAt)
         .ToList();

        var resultDtos = new List<GitHubRepositoryDTO>();

        foreach (var repo in filteredAndSortedRepos)
        {
            var languages = await github.Repository.GetAllLanguages(repo.Id);
            long totalBytes = languages.Sum(lang => lang.NumberOfBytes);

            var languageInfos = new List<LanguageInfo>();
            if (totalBytes > 0)
            {
                languageInfos = languages
                    .OrderByDescending(lang => lang.NumberOfBytes)
                    .Select(lang => new LanguageInfo
                    {
                        Name = lang.Name,
                        Bytes = lang.NumberOfBytes,
                        Color = GetLanguageColor(lang.Name)
                    }).ToList();
            }

            resultDtos.Add(new GitHubRepositoryDTO
            {
                Name = repo.Name,
                Description = repo.Description ?? "No description provided.",
                Url = repo.HtmlUrl,
                Stars = repo.StargazersCount,
                Forks = repo.ForksCount,
                Languages = languageInfos
            });
        }

        return resultDtos;
    }

    private string? GetLanguageColor(string? language)
    {
        if (string.IsNullOrEmpty(language)) return null;

        return language.ToLower() switch
        {
            "c#" => "#178600",
            "javascript" => "#f1e05a",
            "typescript" => "#3178c6",
            "python" => "#3572A5",
            "java" => "#b07219",
            "html" => "#e34c26",
            "css" => "#563d7c",
            "razor" => "#6C1BB1",
            _ => "#8993a2"
        };
    }
}