using MangaWorkflow.Services.HuyNQ.DTOs.User;

namespace MangaWorkflow.Services.HuyNQ;

public interface ISystemUserAccountService
{
    Task<GetUserAccountResponse?> GetUserAccount(GetUserAccountRequest request);
}
