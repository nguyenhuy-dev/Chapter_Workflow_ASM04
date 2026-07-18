using MangaWorkflow.Services.HuyNQ.DTOs.User;

namespace MangaWorkflow.SoapService.HuyNQ.Authentication;

public interface IJwtTokenGenerator
{
    (string Token, DateTime ExpiresAt) GenerateToken(GetUserAccountResponse user);
}
