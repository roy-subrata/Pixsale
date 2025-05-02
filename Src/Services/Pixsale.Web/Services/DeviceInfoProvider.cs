using System.Runtime.InteropServices;
using Pixsale.Shared.Services;

namespace Pixsale.Web.Services
{
    public class DeviceInfoProvider : IDeviceInfoProvider
    {
        public string DeviceName => "Web Browser";

        public string DeviceModel => "N/A";

        public string DeviceManufacturer => "N/A";

        public string DeviceVersion => RuntimeInformation.FrameworkDescription;

        public string DevicePlatform => "Web";

        public string DeviceIdiom => "N/A";

        public string DevicePlatformVersion => RuntimeInformation.OSDescription;

        public override string ToString()
        {
            return $"Device Name: {DeviceName}, Model: {DeviceModel}, Manufacturer: {DeviceManufacturer}, Version: {DeviceVersion}, Platform: {DevicePlatform}, Idiom: {DeviceIdiom}, Platform Version: {DevicePlatformVersion}";
        }
    }
}
