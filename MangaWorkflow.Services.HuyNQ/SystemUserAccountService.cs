using MangaWorkflow.Repositories.HuyNQ;
using MangaWorkflow.Services.HuyNQ.DTOs.User;
using Mapster;

namespace MangaWorkflow.Services.HuyNQ;

public class SystemUserAccountService(SystemUserAccountRepository userRepo) : ISystemUserAccountService
{
    private readonly SystemUserAccountRepository _userRepo = userRepo;

    public async Task<GetUserAccountResponse?> GetUserAccount(GetUserAccountRequest request)
    {
        try
        {
            var user = await Task.Run(() => _userRepo.GetUserAccount(request.UserName, request.Password));

            if (user == null) return null;

            if (user != null && !user.IsActive)
                return null;

            return user.Adapt<GetUserAccountResponse>();
        }
        catch (Exception) { }

        return null;
    }
}
