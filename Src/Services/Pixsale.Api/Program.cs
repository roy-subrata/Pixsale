using Microsoft.EntityFrameworkCore;
using Pixsale.Infrastructure;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddInfrastructureService(builder.Configuration);
// builder.Services.AddDbContext<PixsaleDbContext>(options =>
//     options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))); // Use UseSqlite for SQLite

var app = builder.Build();


app.MapControllers();


if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.MapOpenApi(pattern: "api/document.json");
    app.MapScalarApiReference(options =>
    {
        options.OpenApiRoutePattern = "api/document.json";
        options.Title = "My API Documentation";
        options.Theme = ScalarTheme.Default;
        options.Favicon = "/favicon.svg";
        options.Layout = ScalarLayout.Modern;
        options.DarkMode = true;
        options.CustomCss = "* { font-family: 'Monaco'; }";
        options.Metadata = new Dictionary<string, string>()
        {
            { "ogDescription", "Open Graph description" },
            { "ogTitle", "Open Graph title" },
            { "twitterCard", "summary_large_image" },
            { "twitterSite", "https://example.com/api" },
            { "twitterTitle", "My API documentation" },
            { "twitterDescription", "This is the description for the twitter card" },
            { "twitterImage", "https://dotnet.microsoft.com/blob-assets/images/illustrations/swimlane-build-scalable-web-apps.svg" }
        };
    });
}

else
{
    app.UseHsts();
}

app.MapGet("/", () => "Hello World!");

app.Run();
