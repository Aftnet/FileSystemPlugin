using Foundation;
using Plugin.FileSystem;
using Plugin.FileSystem.Abstractions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Plugin.FileSystem
{
    internal class FileSystem : FileSystemBase
    {
        public override IDirectoryInfo LocalStorage => GetSpecialFolder(Environment.SpecialFolder.LocalApplicationData);

        public override IDirectoryInfo RoamingStorage => GetSpecialFolder(Environment.SpecialFolder.ApplicationData);

        public override IDirectoryInfo InstallLocation => new DirectoryInfo(new System.IO.DirectoryInfo(NSBundle.MainBundle.BundlePath));

        public override Task<IFileInfo> PickFileAsync(IEnumerable<string> extensionsFilter = null)
        {
            throw new NotImplementedException();
        }

        public override Task<IFileInfo[]> PickFilesAsync(IEnumerable<string> extensionsFilter = null)
        {
            throw new NotImplementedException();
        }

        public override Task<IFileInfo> PickSaveFileAsync(string defaultExtension)
        {
            throw new NotImplementedException();
        }

        public override Task<IDirectoryInfo> PickDirectoryAsync()
        {
            throw new NotImplementedException();
        }

        private IDirectoryInfo GetSpecialFolder(Environment.SpecialFolder specialFolder)
        {
            var path = Environment.GetFolderPath(specialFolder);
            var folder = new System.IO.DirectoryInfo(path);
            return new DirectoryInfo(folder);
        }
    }
}