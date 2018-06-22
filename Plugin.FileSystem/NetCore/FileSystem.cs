using Plugin.FileSystem.Abstractions;
using System;
using System.Runtime.InteropServices;

namespace Plugin.FileSystem
{
    internal class FileSystem : FileSystemBase
    {
        public override IDirectoryInfo LocalStorage => GetHomeDirectory();

        public override IDirectoryInfo RoamingStorage => GetHomeDirectory();

        public override IDirectoryInfo InstallLocation => new DirectoryInfo(new System.IO.DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory));

        private IDirectoryInfo GetHomeDirectory()
        {
            var path = Environment.GetEnvironmentVariable(RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? "LocalAppData" : "Home");
            var output = default(IDirectoryInfo);
            if (!string.IsNullOrEmpty(path))
            {
                output = new DirectoryInfo(new System.IO.DirectoryInfo(path));
            }

            return output;
        }
    }
}
