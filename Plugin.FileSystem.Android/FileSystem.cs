using Plugin.FileSystem.Abstractions;

namespace Plugin.FileSystem
{
    internal class FileSystem : FileSystemBase
    {
        public override IDirectoryInfo LocalStorage => new DirectoryInfo(new System.IO.DirectoryInfo(Android.App.Application.Context.FilesDir.AbsolutePath));

        public override IDirectoryInfo RoamingStorage => LocalStorage;

        public override IDirectoryInfo InstallLocation => new DirectoryInfo(new System.IO.DirectoryInfo(Android.App.Application.Context.ApplicationInfo.DataDir));
    }
}