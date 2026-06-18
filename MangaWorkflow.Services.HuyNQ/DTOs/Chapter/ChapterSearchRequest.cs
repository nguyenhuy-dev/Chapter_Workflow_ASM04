namespace MangaWorkflow.Services.HuyNQ.DTOs.Chapter;

public record ChapterSearchRequest(string? Title, int? ChapterNumber, bool? Approved)
{
}
