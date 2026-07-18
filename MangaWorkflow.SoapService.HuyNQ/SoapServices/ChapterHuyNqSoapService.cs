using MangaWorkflow.Services.HuyNQ;
using MangaWorkflow.Services.HuyNQ.DTOs.Chapter;
using MangaWorkflow.SoapService.HuyNQ.SoapModels;
using System.ComponentModel.DataAnnotations;
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
            if (request is null)
            {
                Console.WriteLine("Request is required.");
                return 0;
            }

            var opt = new JsonSerializerOptions() { ReferenceHandler = ReferenceHandler.IgnoreCycles, DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull };

            var itemJsonString = JsonSerializer.Serialize(request, opt);

            var dto = JsonSerializer.Deserialize<ChapterCreateRequest>(itemJsonString) ?? new();

            if (!TryValidate(dto, out var errors))
            {
                Console.WriteLine($"Validation failed: {string.Join("; ", errors)}");
                return 0;
            }

            var result = await _chapterService.CreateAsync(dto);

            return result;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }

        return 0;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        try
        {
            var result = await _chapterService.DeleteAsync(id);

            return result;
        } 
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }

        return false;
    }

    public async Task<int> UpdateAsync(ChapterHuyNq request)
    {
        try
        {
            if (request is null)
            {
                Console.WriteLine("Request is required.");
                return 0;
            }

            if (request.HuynqId <= 0)
            {
                Console.WriteLine("A valid HuynqId is required for update.");
                return 0;
            }

            var opt = new JsonSerializerOptions() { ReferenceHandler = ReferenceHandler.IgnoreCycles, DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull };

            var itemsJsonString = JsonSerializer.Serialize(request, opt);

            var dto = JsonSerializer.Deserialize<ChapterUpdateRequest>(itemsJsonString) ?? new();

            if (!TryValidate(dto, out var errors))
            {
                Console.WriteLine($"Validation failed: {string.Join("; ", errors)}");
                return 0;
            }

            var result = await _chapterService.UpdateAsync(request.HuynqId, dto);

            return result;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }

        return 0;
    }

    // Chạy DataAnnotations attribute trên DTO. SOAP/WCF không tự validate như Web API,
    // nên phải gọi Validator.TryValidateObject thủ công thì attribute mới có tác dụng.
    private static bool TryValidate(object model, out List<string> errors)
    {
        var context = new ValidationContext(model);
        var results = new List<ValidationResult>();

        var isValid = Validator.TryValidateObject(model, context, results, validateAllProperties: true);

        errors = results.Select(r => r.ErrorMessage ?? "Invalid value.").ToList();

        return isValid;
    }
}
