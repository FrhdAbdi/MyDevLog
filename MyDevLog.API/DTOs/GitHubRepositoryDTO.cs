namespace MyDevLog.API.DTOs;


public class GitHubRepositoryDTO
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string Url { get; set; }
    public int Stars { get; set; }
    public int Forks { get; set; }
    public string PrimaryLanguage { get; set; }
    public List<LanguageInfo> Languages { get; set; } = new();
}