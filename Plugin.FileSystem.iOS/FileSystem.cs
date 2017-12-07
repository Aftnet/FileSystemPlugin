using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Foundation;
using Plugin.FileSystem.Abstractions;

namespace Plugin.FileSystem
{
    internal class FileSystem : FileSystemBase
    {
        public override IDirectoryInfo LocalStorage => GetSpecialFolder(Environment.SpecialFolder.LocalApplicationData);

        public override IDirectoryInfo RoamingStorage => LocalStorage;

        public override IDirectoryInfo InstallLocation => new DirectoryInfo(new System.IO.DirectoryInfo(NSBundle.MainBundle.BundlePath));

        private IDirectoryInfo GetSpecialFolder(Environment.SpecialFolder specialFolder)
        {
            var path = Environment.GetFolderPath(specialFolder);
            var folder = new System.IO.DirectoryInfo(path);
            return new DirectoryInfo(folder);
        }
    }
}