using Microsoft.Extensions.Logging;
using MudBlazor.Services;
using Pixsale.Services;
using Pixsale.Shared.Clients;
using Pixsale.Shared.Services;

namespace Pixsale;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            });

        // Add device-specific services used by the Pixsale.Shared project
        builder.Services.AddSingleton<IFormFactor, FormFactor>();

        builder.Services.AddMauiBlazorWebView();
        builder.Services.AddMudServices();
#if DEBUG
        builder.Services.AddBlazorWebViewDeveloperTools();
        builder.Logging.AddDebug();
#endif

        builder.Services.AddApiClient();

        return builder.Build();
    }
}
