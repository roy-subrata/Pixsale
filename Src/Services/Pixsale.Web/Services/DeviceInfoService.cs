using Microsoft.JSInterop;
using Pixsale.Shared.Services;

namespace Pixsale.Services;

public class DeviceInfoService : IDeviceInfoService
{
    private readonly IDeviceInfoProvider _deviceInfo;
    private readonly ILogger<DeviceInfoService> _logger;
    private readonly IJSRuntime _jSRuntime;
    public DeviceInfoService(IDeviceInfoProvider deviceInfo, ILogger<DeviceInfoService> logger, IJSRuntime jSRuntime)
    {
        _deviceInfo = deviceInfo ?? throw new ArgumentNullException(nameof(deviceInfo));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _jSRuntime = jSRuntime ?? throw new ArgumentNullException(nameof(jSRuntime));
    }
    public ApiConfiguration GetApiConfiguration()
    {
        try
        {
            return GetPlatform() switch
            {
                "Web" => new ApiConfiguration
                {
                    BaseUrl = " http://localhost:5262",
                    Headers = new Dictionary<string, string>
                    {
                        { "Authorization", "Bearer <token>" },
                        { "Accept", "application/json" }
                    },
                    TimeoutSeconds = 30
                },
                _ => new ApiConfiguration { BaseUrl = "https://api.default.example.com", TimeoutSeconds = 15 }
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to retrieve API configuration.");
            return new ApiConfiguration { BaseUrl = "https://api.fallback.example.com", TimeoutSeconds = 15 };
        }

    }

    public string GetFormFactor()
    {
        return "Desktop";
    }

    public string GetPlatform()
    {
        return _deviceInfo.DevicePlatform.Equals("Web", StringComparison.OrdinalIgnoreCase)
        ? "Web"
        : "Unknown";
    }

    public Version GetPlatformVersion()
    {
        try
        {
            return Version.TryParse(_deviceInfo.DevicePlatformVersion, out var version)
                ? version
                : Environment.OSVersion.Version;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to retrieve platform version.");
            return Environment.OSVersion.Version;
        }
    }

    public RenderConfiguration GetRenderConfiguration()
    {
        return new RenderConfiguration
        {
            PreferredLayout = "Grid",
            FontScale = 1.0,
            SupportsDarkMode = true
        };
    }

    public async Task<bool> IsDevice(FormFactor formFactor)
    {
        try
        {
            var result =  await _jSRuntime.InvokeAsync<string>("deviceInfo.getFormFactor");

            return formFactor switch
            {
                FormFactor.Phone => result.Equals("Phone", StringComparison.OrdinalIgnoreCase),
                FormFactor.Tablet => result.Equals("Tablet", StringComparison.OrdinalIgnoreCase),
                FormFactor.Desktop => result.Equals("Desktop", StringComparison.OrdinalIgnoreCase),
                _ => false
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to determine device form factor.");
            return false;
        }
       
    }


    public bool IsPlatform(PlatformType platform)
    {
        return platform switch
        {
            PlatformType.Web => _deviceInfo.DevicePlatform.Equals("Web", StringComparison.OrdinalIgnoreCase),
            _ => false
        };
    }

    public override string ToString()
    {
        return $"Platform: {GetPlatform()}, Version: {GetPlatformVersion()}, Form Factor: {GetFormFactor()}";
    }
}




