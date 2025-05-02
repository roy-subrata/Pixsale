namespace Pixsale.Shared.Services
{
    public interface IDeviceInfoService
    {
        string GetFormFactor(); // e.g., Phone, Tablet, Desktop
        string GetPlatform(); // e.g., Android, iOS, Windows
                              //  Version GetPlatformVersion(); // Structured version info
        bool IsPlatform(PlatformType platform); // Check specific platform
                                                //  Task<FeatureSupport> GetFeatureSupportAsync(string feature); // Check feature availability
        Task<ApiConfiguration> GetApiConfigurationAsync(); // Platform-specific API settings
        Task<RenderConfiguration> GetRenderConfigurationAsync(); // Platform-specific UI rendering
    }

    public class FeatureSupport
    {
        public bool IsSupported { get; set; }
        public string Reason { get; set; }
    }

    public class ApiConfiguration
    {
        public string BaseUrl { get; set; }
        public Dictionary<string, string> Headers { get; set; }
        public int TimeoutSeconds { get; set; }
    }

    public class RenderConfiguration
    {
        public string PreferredLayout { get; set; } // e.g., "Grid" or "Stack"
        public double FontScale { get; set; } // Adjust for platform
        public bool SupportsDarkMode { get; set; }
    }
}
