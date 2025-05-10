namespace Pixsale.Shared.Services;
public interface IDeviceInfoProvider
{
    string DeviceName { get; }
    string DeviceModel { get; }
    string DeviceManufacturer { get; }
    string DeviceVersion { get; }
    string DevicePlatform { get; }
    string DeviceIdiom { get; }
    string DevicePlatformVersion { get; }

}


