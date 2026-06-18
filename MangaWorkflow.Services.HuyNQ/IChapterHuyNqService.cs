using MangaWorkflow.Entities.HuyNQ.Models;
using MangaWorkflow.Services.HuyNQ.DTOs.Chapter;

namespace MangaWorkflow.Services.HuyNQ;

public interface IChapterHuyNqService
{
    Task<List<ChapterHuyNq>> GetAllAsync();
    Task<ChapterGetByIdResponse?> GetByIdAsync(int id);
    Task<List<ChapterHuyNq>> SearchAsync(ChapterSearchRequest request);

    Task<int> CreateAsync(ChapterCreateRequest chapter);
    Task<int> UpdateAsync(int id, ChapterUpdateRequest chapter);
    Task<bool> DeleteAsync(int id);
}
