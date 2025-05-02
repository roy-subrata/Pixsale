using Microsoft.Extensions.Logging;
using Pixsale.Shared.Services;

namespace Pixsale.Services;

public class DeviceInfoService : IDeviceInfoService
{
    private readonly IDeviceInfoProvider _deviceInfo;
    private readonly ILogger<DeviceInfoService> _logger;
    private static readonly Dictionary<string, FeatureSupport> _featureCache = new();
    private static readonly SemaphoreSlim _cacheLock = new(1, 1);

    public DeviceInfoService(IDeviceInfoProvider deviceInfo, ILogger<DeviceInfoService> logger)
    {
        _deviceInfo = deviceInfo ?? throw new ArgumentNullException(nameof(deviceInfo));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }
    public async Task<ApiConfiguration> GetApiConfigurationAsync()
    {
        try
        {
            return await Task.FromResult(_deviceInfo.DevicePlatform.ToLower() switch
            {
                "android" => new ApiConfiguration
                {
                    BaseUrl = "https://api.android.example.com",
                    Headers = new Dictionary<string, string> { { "X-Platform", "Android" } },
                    TimeoutSeconds = 30
                },
                "ios" => new ApiConfiguration
                {
                    BaseUrl = "https://api.ios.example.com",
                    Headers = new Dictionary<string, string> { { "X-Platform", "iOS" } },
                    TimeoutSeconds = 20
                },
                _ => new ApiConfiguration
                {
                    BaseUrl = "https://api.default.example.com",
                    Headers = new Dictionary<string, string>(),
                    TimeoutSeconds = 15
                }
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to retrieve API configuration.");
            return new ApiConfiguration { BaseUrl = "https://api.fallback.example.com", TimeoutSeconds = 15 };
        }
    }


    public string GetFormFactor()
    {
        try
        {
            return _deviceInfo.DeviceIdiom;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to retrieve form factor.");
            return "Unknown";
        }
    }

    public string GetPlatform()
    {
        try
        {
            return _deviceInfo.DeviceVersion;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to retrieve platform info.");
            return "Unknown";
        }
    }


    public async Task<RenderConfiguration> GetRenderConfigurationAsync()
    {
        try
        {
            // Example: Platform-specific UI rendering
            return await Task.FromResult(_deviceInfo.DevicePlatform.ToLower() switch
            {
                "android" => new RenderConfiguration
                {
                    PreferredLayout = "Grid",
                    FontScale = 1.2,
                    SupportsDarkMode = true
                },
                "ios" => new RenderConfiguration
                {
                    PreferredLayout = "Stack",
                    FontScale = 1.0,
                    SupportsDarkMode = true
                },
                _ => new RenderConfiguration
                {
                    PreferredLayout = "Stack",
                    FontScale = 1.0,
                    SupportsDarkMode = false
                }
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to retrieve render configuration.");
            return new RenderConfiguration { PreferredLayout = "Stack", FontScale = 1.0, SupportsDarkMode = false };
        }
    }

    public bool IsPlatform(PlatformType platform)
    {
        var platformString = _deviceInfo.DevicePlatform.ToLower();
        return platform switch
        {
            PlatformType.Android => platformString.Contains("android"),
            PlatformType.iOS => platformString.Contains("ios"),
            PlatformType.Windows => platformString.Contains("windows"),
            PlatformType.MacCatalyst => platformString.Contains("maccatalyst"),
            _ => false
        };
    }
    public override string ToString()
    {
        return $"Device Name: {_deviceInfo.DeviceName}, Model: {_deviceInfo.DeviceModel}, Manufacturer: {_deviceInfo.DeviceManufacturer}, Version: {_deviceInfo.DeviceVersion}, Platform: {_deviceInfo.DevicePlatform}, Idiom: {_deviceInfo.DeviceIdiom}, Platform Version: {_deviceInfo.DevicePlatformVersion}";
    }
}




