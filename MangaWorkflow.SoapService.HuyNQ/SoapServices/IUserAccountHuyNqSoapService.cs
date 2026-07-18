using MangaWorkflow.SoapService.HuyNQ.SoapModels;
using System.ServiceModel;

namespace MangaWorkflow.SoapService.HuyNQ.SoapServices;

[ServiceContract]
public interface IUserAccountHuyNqSoapService
{
    [OperationContract]
    Task<UserAccountHuyNq> LoginAsync(LoginHuyNqRequest request);
}
