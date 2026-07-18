using MangaWorkflow.Services.HuyNQ;
using MangaWorkflow.Services.HuyNQ.DTOs.User;
using MangaWorkflow.SoapService.HuyNQ.Authentication;
using MangaWorkflow.SoapService.HuyNQ.SoapModels;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MangaWorkflow.SoapService.HuyNQ.SoapServices;

public class UserAccountHuyNqSoapService(ISystemUserAccountService userService, IJwtTokenGenerator jwtTokenGenerator) : IUserAccountHuyNqSoapService
{
    private readonly ISystemUserAccountService _userService = userService;
    private readonly IJwtTokenGenerator _jwtTokenGenerator = jwtTokenGenerator;

    public async Task<UserAccountHuyNq> LoginAsync(LoginHuyNqRequest request)
    {
        try
        {
            if (request is null)
            {
                Console.WriteLine("Request is required.");
                return null;
            }

            if (!TryValidate(request, out var errors))
            {
                Console.WriteLine($"Validation failed: {string.Join("; ", errors)}");
                return null;
            }

            var user = await _userService.GetUserAccount(new GetUserAccountRequest
            {
                UserName = request.Email,
                Password = request.Password
            });

            if (user is null)
            {
                Console.WriteLine("Invalid username or password.");
                return null;
            }

            var opt = new JsonSerializerOptions() { ReferenceHandler = ReferenceHandler.IgnoreCycles, DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull };

            var userJsonString = JsonSerializer.Serialize(user, opt);

            var response = JsonSerializer.Deserialize<UserAccountHuyNq>(userJsonString);

            if (response is not null)
            {
                var (token, expiresAt) = _jwtTokenGenerator.GenerateToken(user);
                response.AccessToken = token;
                response.TokenExpiresAt = expiresAt;
            }

            return response;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }

        return null;
    }

    // Chạy DataAnnotations attribute trên request. SOAP/WCF không tự validate như Web API,
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
