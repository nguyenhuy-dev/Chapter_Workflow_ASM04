namespace MangaWorkflow.SoapService.HuyNQ.Authentication;

// SoapCore không đọc [Authorize] trên [OperationContract], nên chặn thủ công bằng middleware.
// Các path trong _protectedPaths yêu cầu JWT hợp lệ; những path khác (vd /User.asmx để login) để mở.
public class SoapJwtAuthMiddleware(RequestDelegate next)
{
    private readonly RequestDelegate _next = next;

    private static readonly string[] _protectedPaths =
    [
        "/Chapter.asmx"
    ];

    public async Task InvokeAsync(HttpContext context)
    {
        var path = context.Request.Path;

        var isProtected = _protectedPaths.Any(p =>
            path.StartsWithSegments(p, StringComparison.OrdinalIgnoreCase));

        if (isProtected && context.User?.Identity?.IsAuthenticated != true)
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            context.Response.ContentType = "text/plain";
            await context.Response.WriteAsync("Unauthorized: a valid Bearer token is required.");
            return;
        }

        await _next(context);
    }
}
