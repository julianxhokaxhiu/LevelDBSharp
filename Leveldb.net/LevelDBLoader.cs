using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;

namespace LevelDB
{
    internal static class LevelDbLoader
    {
        private const string LIB_NAME = "libleveldb";

        private static bool _loaded = false;
        private static readonly object _lock = new object();

        public static void EnsureLoaded()
        {
            if (_loaded) return;

            lock (_lock)
            {
                if (_loaded) return;

                NativeLibrary.SetDllImportResolver(typeof(LevelDBInterop).Assembly, Resolver);
                _loaded = true;
            }
        }

        private static IntPtr Resolver(string libraryName, Assembly assembly, DllImportSearchPath? searchPath)
        {
            if (libraryName != LIB_NAME)
                return IntPtr.Zero;

            string libFileName = GetLibFileName();
            string rid = GetRuntimeIdentifier();
            string basePath = AppContext.BaseDirectory;

            string fullPath = Path.Combine(basePath, "runtimes", rid, libFileName);

            if (!File.Exists(fullPath))
                throw new DllNotFoundException($"Cannot find native library: {fullPath}");

            return NativeLibrary.Load(fullPath);
        }

        private static string GetLibFileName()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) return "leveldb.dll";
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux)) return "libleveldb.so";
            if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX)) return "libleveldb.dylib";
            throw new PlatformNotSupportedException("Unsupported OS");
        }

        private static string GetRuntimeIdentifier()
        {
            var arch = RuntimeInformation.ProcessArchitecture switch
            {
                Architecture.X64 => "x64",
                Architecture.X86 => "x86",
                Architecture.Arm64 => "arm64",
                Architecture.Arm => "arm",
                _ => throw new NotSupportedException("Unsupported architecture")
            };

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) return $"{arch}-windows";
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux)) return $"{arch}-linux";
            if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX)) return $"{arch}-osx";

            throw new PlatformNotSupportedException("Unsupported OS");
        }
    }
}