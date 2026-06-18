using MangaWorkflow.Services.HuyNQ;
using MangaWorkflow.Services.HuyNQ.DTOs.Chapter;
using MangaWorkflow.SoapService.HuyNQ.SoapModels;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MangaWorkflow.SoapService.HuyNQ.SoapServices;

public class ChapterHuyNqSoapService(IChapterHuyNqService chapterService) : IChapterHuyNqSoapService
{
    private readonly IChapterHuyNqService _chapterService = chapterService;

    public async Task<List<ChapterHuyNq>> GetAllAsync()
    {
        try
        {
            var items = await _chapterService.GetAllAsync();

            var opt = new JsonSerializerOptions() { ReferenceHandler = ReferenceHandler.IgnoreCycles, DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull };

            var itemsJsonString = JsonSerializer.Serialize(items, opt);

            return JsonSerializer.Deserialize<List<ChapterHuyNq>>(itemsJsonString) ?? [];
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }

        return [];
    }

    public async Task<ChapterHuyNq> GetByIdAsync(int id)
    {
        try
        {
            var opt = new JsonSerializerOptions() { ReferenceHandler = ReferenceHandler.IgnoreCycles, DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull };

            var item = await _chapterService.GetByIdAsync(id);

            var itemJsonString = JsonSerializer.Serialize(item, opt);

            return JsonSerializer.Deserialize<ChapterHuyNq>(itemJsonString) ?? new();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }

        return new();
    }

    public async Task<List<ChapterHuyNq>> SearchAsync(string? title, int? chapterNumber, bool? approved)
    {
        try
        {
            var opt = new JsonSerializerOptions() { ReferenceHandler = ReferenceHandler.IgnoreCycles, DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull };

            var items = await _chapterService.SearchAsync(new Services.HuyNQ.DTOs.Chapter.ChapterSearchRequest(title, chapterNumber, approved));

            var itemsJsonString = JsonSerializer.Serialize(items, opt);

            var results = JsonSerializer.Deserialize<List<ChapterHuyNq>>(itemsJsonString) ?? [];

            return results;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }

        return [];
    }

    public async Task<int> CreateAsync(ChapterHuyNq request)
    {
        try
        {
            var opt = new JsonSerializerOptions() { ReferenceHandler = ReferenceHandler.IgnoreCycles, DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull };

            var itemJsonString = JsonSerializer.Serialize(request, opt);

            var result = await _chapterService.CreateAsync(JsonSerializer.Deserialize<ChapterCreateRequest>(itemJsonString) ?? new());

            return result;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }

        return 0;
    }

    public Task<bool> DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<int> UpdateAsync(ChapterCreateRequest request)
    {
        throw new NotImplementedException();
    }
}
