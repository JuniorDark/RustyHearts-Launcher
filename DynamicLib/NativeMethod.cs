using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

public static class NativeMethod
{
    [DllImport("kernel32.dll", EntryPoint = "LoadLibrary")]
    public static extern int LoadLibrary(
        [MarshalAs(UnmanagedType.LPStr)] string lpLibFileName);

    [DllImport("kernel32.dll", EntryPoint = "GetProcAddress")]
    public static extern IntPtr GetProcAddress(int hModule,
        [MarshalAs(UnmanagedType.LPStr)] string lpProcName);

    [DllImport("kernel32.dll", EntryPoint = "FreeLibrary")]
    public static extern bool FreeLibrary(int hModule);
}
