using System.Runtime.InteropServices;
using Pixsale.Shared.Services;

namespace Pixsale.Services;

public class DeviceInfoService : IDeviceInfoService
{
    public Task<ApiConfiguration> GetApiConfigurationAsync()
    {
        return Task.FromResult(new ApiConfiguration
        {
            BaseUrl = "https://api.example.com",
            Headers = new Dictionary<string, string>
            {
                { "Authorization", "Bearer <token>" },
                { "Accept", "application/json" }
            },
            TimeoutSeconds = 30
        });
    }

    public string GetFormFactor()
    {
        // Example: Assume web is always "Desktop" for simplicity
        return "Desktop";
    }

    public string GetPlatform()
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            return "Windows";
        if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            return "MacOS";
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            return "Linux";

        return "Unknown";
    }

    public Version GetPlatformVersion()
    {
        return Environment.OSVersion.Version;
    }

    public Task<RenderConfiguration> GetRenderConfigurationAsync()
    {
        return Task.FromResult(new RenderConfiguration
        {
            PreferredLayout = "Grid",
            FontScale = 1.0,
            SupportsDarkMode = true
        });
    }

    public bool IsPlatform(PlatformType platform)
    {
        return platform switch
        {
            PlatformType.Windows => RuntimeInformation.IsOSPlatform(OSPlatform.Windows),
            PlatformType.MacCatalyst => RuntimeInformation.IsOSPlatform(OSPlatform.OSX),
            PlatformType.Android => false, // Not applicable for web
            PlatformType.iOS => false, // Not applicable for web
            _ => false
        };
    }

    public override string ToString()
    {
        return $"Platform: {GetPlatform()}, Version: {GetPlatformVersion()}, Form Factor: {GetFormFactor()}";
    }
}




