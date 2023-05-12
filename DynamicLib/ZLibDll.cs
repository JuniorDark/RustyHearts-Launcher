using System;
using System.Text;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.IO;
using RHLauncher.DynamicLib;

public static class ZLibDll
{

    //[DllImport("ZlibDll.dll", CallingConvention = CallingConvention.Cdecl)]
    //public static extern int Decompress(byte[] compressed_buffer, int compressed_size, byte[] decompressed_buffer, ref int decompressed_size);

    public delegate int DecompressDelegate(byte[] compressed_buffer, int compressed_size, byte[] decompressed_buffer, ref int decompressed_size);
    public static DecompressDelegate? Decompress = null;

    //[DllImport("ZlibDll.dll", CallingConvention = CallingConvention.Cdecl)]
    //public static extern int Compress(byte[] decompressed_buffer, int decompressed_size, byte[] compressed_buffer, ref int compressed_size);
    public delegate int CompressDelegate(byte[] decompressed_buffer, int decompressed_size, byte[] compressed_buffer, ref int compressed_size);
    public static CompressDelegate? Compress = null;

    static ZLibDll()
    {
        byte[] libBuffer = ResourceDll.ZlibDll;
        string dllPath = Path.Combine(Path.GetTempPath(), "ZlibDll.dll");
        try
        {
            File.WriteAllBytes(dllPath, libBuffer);
        }
        catch { }

        int hModule = NativeMethod.LoadLibrary(dllPath);
        if (hModule == 0) return;

        IntPtr intPtr = NativeMethod.GetProcAddress(hModule, "Decompress");
        Decompress = (DecompressDelegate)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(DecompressDelegate));

        intPtr = NativeMethod.GetProcAddress(hModule, "Compress");
        Compress = (CompressDelegate)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(CompressDelegate));
    }
}