var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHealthChecks();
builder.Services.AddProblemDetails();

var indexPath = Path.Combine(builder.Environment.ContentRootPath, "wwwroot", "index.html");
if (!File.Exists(indexPath))
{
    throw new InvalidOperationException(
        "The Vue build is missing. Run npm ci and npm run build in src/gibson-coffee-works-web.");
}

var app = builder.Build();

app.UseExceptionHandler();
app.UseHttpsRedirection();
app.UseDefaultFiles();
app.UseStaticFiles();
app.MapHealthChecks("/health/live");

// Reserved backend paths must never fall through to a successful SPA response.
app.MapFallback("/api/{**path}", () => Results.Problem(statusCode: StatusCodes.Status404NotFound));
app.MapFallback("/auth/{**path}", () => Results.Problem(statusCode: StatusCodes.Status404NotFound));
app.MapFallbackToFile("index.html");

app.Run();

/// <summary>Exposes the BFF entry point to integration tests.</summary>
public partial class Program;
