using System.ComponentModel.DataAnnotations;

namespace MangaWorkflow.Services.HuyNQ.DTOs.Chapter;

public class ChapterCreateRequest
{
    [Required(ErrorMessage = "ChapterMetaHuynqId is required.")]
    [Range(1, int.MaxValue, ErrorMessage = "ChapterMetaHuynqId must be greater than 0.")]
    public int? ChapterMetaHuynqId { get; set; }

    [Required(AllowEmptyStrings = false, ErrorMessage = "Title is required.")]
    [StringLength(255, ErrorMessage = "Title must not exceed 255 characters.")]
    public string Title { get; set; }

    [Required(ErrorMessage = "ChapterNumber is required.")]
    [Range(1, int.MaxValue, ErrorMessage = "ChapterNumber must be greater than 0.")]
    public int? ChapterNumber { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "PageCount must be greater than 0.")]
    public int? PageCount { get; set; }

    public bool? Approved { get; set; }

    public DateTime? ReleaseDate { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "PriorityLevel must not be negative.")]
    public int? PriorityLevel { get; set; }

    public string EditorComment { get; set; }

    public string LayoutVersion { get; set; }

    public string ReviewNotes { get; set; }
}
