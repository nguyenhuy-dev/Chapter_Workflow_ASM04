using MangaWorkflow.Services.HuyNQ.DTOs.ChapterMeta;

namespace MangaWorkflow.Services.HuyNQ.DTOs.Chapter;

public class ChapterGetByIdResponse
{
    public int HuynqId { get; set; }

    public int? ChapterMetaHuynqId { get; set; }

    public string Title { get; set; }

    public int? ChapterNumber { get; set; }

    public int? PageCount { get; set; }

    public bool? Approved { get; set; }

    public DateTime? ReleaseDate { get; set; }

    public int? PriorityLevel { get; set; }

    public string EditorComment { get; set; }

    public string LayoutVersion { get; set; }

    public string ReviewNotes { get; set; }

    public ChapterMetaRefResponse ChapterMeta { get; set; }
}
