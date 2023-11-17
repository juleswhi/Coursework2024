using System.Runtime.InteropServices;

namespace PhysicsEngine;

internal static class CInterop
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Message
    {
        public IntPtr hWind;
        public IntPtr wParam;
        public IntPtr lParam;
        public uint time;
        public System.Drawing.Point p;
    }

    const uint QS_MASK = 0x1FF;

    [System.Security.SuppressUnmanagedCodeSecurity]
    [System.Runtime.InteropServices.DllImport("user32.dll")]
    static extern uint GetQueueStatus(uint flags);

    public static bool IsApplicationIdle()
    {
        return 0 == (GetQueueStatus(QS_MASK) >> 16 & QS_MASK);
    }
}
