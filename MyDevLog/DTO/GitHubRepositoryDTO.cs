namespace MyDevLog.DTO;


public class GitHubRepositoryDTO
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
    public int Stars { get; set; }
    public int Forks { get; set; }
    public string? PrimaryLanguage { get; set; }
    public List<LanguageInfo> Languages { get; set; } = new();
}