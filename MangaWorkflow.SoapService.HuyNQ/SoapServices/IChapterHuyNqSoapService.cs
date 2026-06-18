using MangaWorkflow.Services.HuyNQ.DTOs.Chapter;
using MangaWorkflow.SoapService.HuyNQ.SoapModels;
using System.ServiceModel;

namespace MangaWorkflow.SoapService.HuyNQ.SoapServices;

[ServiceContract]
public interface IChapterHuyNqSoapService
{
    // Queries
    [OperationContract]
    Task<List<ChapterHuyNq>> GetAllAsync();

    [OperationContract]
    Task<ChapterHuyNq> GetByIdAsync(int id);

    [OperationContract]
    Task<List<ChapterHuyNq>> SearchAsync(ChapterSearchRequest request);

    // Mutations
    [OperationContract]
    Task<int> CreateAsync(ChapterHuyNq request);

    [OperationContract]
    Task<int> UpdateAsync(ChapterCreateRequest request);

    [OperationContract]
    Task<bool> DeleteAsync(int id);
}
