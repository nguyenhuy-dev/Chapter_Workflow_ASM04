using MangaWorkflow.Entities.HuyNQ.Models;

namespace MangaWorkflow.Services.HuyNQ;

public interface IChapterMetaHuyNqService
{
    Task<List<ChapterMetaHuyNq>> GetAllAsync();
}
