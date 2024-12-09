using DeviceId;

namespace cloudVault.Classes
{
    internal sealed class MachineManager
    {
        public static string GetDeviceInfo()
        {
            return new DeviceIdBuilder()
                .AddMachineName()
                .AddMacAddress()
                .AddOsVersion()
                .AddUserName()
                .ToString();
        }
    }
}
