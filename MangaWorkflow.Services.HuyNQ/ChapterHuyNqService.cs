using MangaWorkflow.Entities.HuyNQ.Models;
using MangaWorkflow.Repositories.HuyNQ;
using MangaWorkflow.Services.HuyNQ.DTOs.Chapter;
using Mapster;

namespace MangaWorkflow.Services.HuyNQ;

public class ChapterHuyNqService(ChapterHuyNqRepository chapterRepo) : IChapterHuyNqService
{
    private readonly ChapterHuyNqRepository _chapterRepo = chapterRepo;

    public async Task<int> CreateAsync(ChapterCreateRequest chapter)
    {
        try
        {
            var item = chapter.Adapt<ChapterHuyNq>();
            return await _chapterRepo.CreateAsync(item);
        }
        catch
        {
            throw;
        }
    }

    public async Task<bool> DeleteAsync(int id)
    {
        try
        {
            var item = await _chapterRepo.GetByIdAsync(id);
            if (item == null)
                return false;
            return await _chapterRepo.RemoveAsync(item);
        }
        catch 
        {
            throw;
        }
    }

    public async Task<List<ChapterHuyNq>> GetAllAsync()
    {
        try
        {
            return await _chapterRepo.GetAllAsync();
        }
        catch
        {
            throw;
        }
    }

    public async Task<ChapterGetByIdResponse?> GetByIdAsync(int id)
    {
        try
        {
            var item = await _chapterRepo.GetByIdAsync(id);
            var response = item.Adapt<ChapterGetByIdResponse>();
            return response;
        }
        catch
        {
            throw;
        }
    }

    public async Task<List<ChapterHuyNq>> SearchAsync(ChapterSearchRequest request)
    {
        try
        {
            return await _chapterRepo.SearchAsync(request.Title, request.ChapterNumber, request.Approved);
        }
        catch
        {
            throw;
        }
    }

    public async Task<int> UpdateAsync(int id, ChapterUpdateRequest chapter)
    {
        if (chapter.ChapterMetaHuynqId == null)
        {
            Console.WriteLine("ChapterMetaHuynqId is required for update.");
            return 0;
        }

        try
        {
            var existingChapter = await _chapterRepo.GetByIdAsync(id);
            if (existingChapter == null)
            {
                Console.WriteLine("Chapter is not found.");
                return 0;
            }

            chapter.Adapt(existingChapter);
            return await _chapterRepo.UpdateAsync(existingChapter);
        }
        catch
        {
            throw;
        }
    }
}
