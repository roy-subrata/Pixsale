using Pixsale.Shared.Services;

namespace Pixsale.Services
{
    public class DeviceInfoProvider : IDeviceInfoProvider
    {
        public string DeviceName => DeviceInfo.Name;

        public string DeviceModel => DeviceInfo.Model;

        public string DeviceManufacturer => DeviceInfo.Manufacturer;

        public string DeviceVersion => DeviceInfo.VersionString;

        public string DevicePlatform => DeviceInfo.Platform.ToString();

        public string DeviceIdiom => DeviceInfo.Idiom.ToString();

        public string DevicePlatformVersion => DeviceInfo.Version.ToString();

        public override string ToString()
        {
            return $"Device Name: {DeviceName}, Model: {DeviceModel}, Manufacturer: {DeviceManufacturer}, Version: {DeviceVersion}, Platform: {DevicePlatform}, Idiom: {DeviceIdiom}, Platform Version: {DevicePlatformVersion}";
        }
    }
}
