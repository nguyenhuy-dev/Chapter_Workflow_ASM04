namespace MangaWorkflow.Services.HuyNQ.DTOs.User;

public class GetUserAccountRequest
{
    public string UserName { get; set; } = default!;

    public string Password { get; set; } = default!;
}
