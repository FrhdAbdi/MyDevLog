namespace MyDevLog.API.Data.Entities;


public class Post
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string CoverImageFileName { get; set; } = string.Empty;
    public DateTime PublishedDate { get; set; }
    public int ReadingTimeInMinutes { get; set; }
    public string Content { get; set; } = string.Empty;
    public string Summary { get; set; } = string.Empty;
}
