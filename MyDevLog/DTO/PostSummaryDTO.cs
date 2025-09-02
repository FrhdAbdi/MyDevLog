namespace MyDevLog.DTO;


public class PostSummaryDTO
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Summary { get; set; } = string.Empty;
    public DateTime PublishedDate { get; set; }
}
