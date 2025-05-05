using System.Runtime.InteropServices;

public static class DisplayDevices
{
    /**
    <summary>DISPLAY_DEVICEW
    <see href="https://learn.microsoft.com/en-us/windows/win32/api/wingdi/ns-wingdi-display_devicew"/>
    </summary>
    */
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public readonly struct DISPLAY_DEVICEW
    {
        [MarshalAs(UnmanagedType.U4)]
        private readonly uint cb = (uint)Marshal.SizeOf<DISPLAY_DEVICEW>();
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public readonly string DeviceName = "";
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
        public readonly string DeviceString = "";
        [MarshalAs(UnmanagedType.U4)]
        public readonly uint StateFlags;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
        public readonly string DeviceID = "";
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
        public readonly string DeviceKey = "";

        internal DISPLAY_DEVICEW Clone() => (DISPLAY_DEVICEW)MemberwiseClone();

        public DISPLAY_DEVICEW() { }
    }

    /**
    <summary>EnumDisplayDevicesW
    <see href="https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-enumdisplaydevicesw"/>
    </summary>
    */
    [DllImport("user32.dll", CharSet = CharSet.Unicode)]
    static extern bool EnumDisplayDevicesW(string? lpDevice, uint iDevNum, ref DISPLAY_DEVICEW lpDisplayDevice, uint dwFlags);


    public static IEnumerable<DISPLAY_DEVICEW> GetAll(uint getInterfaceName = 0)
    {
        DISPLAY_DEVICEW curr = new();
        for (uint i = 0; EnumDisplayDevicesW(null, i, ref curr, getInterfaceName); ++i)
            for (uint j = 0; EnumDisplayDevicesW(curr.DeviceName, j, ref curr, getInterfaceName); ++j)
                yield return curr.Clone();
    }

    public static IEnumerable<DISPLAY_DEVICEW> FromID(string id = "", uint getInterfaceName = 0)
    {
        string prefix = getInterfaceName == 0 ? @"MONITOR\" : @"\\?\DISPLAY#";
        foreach (DISPLAY_DEVICEW device in GetAll(getInterfaceName))
            if (device.DeviceID.StartsWith(prefix + id))
                yield return device;
    }
}
