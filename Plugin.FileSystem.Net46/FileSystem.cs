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

        public override IDirectoryInfo InstallLocation => new DirectoryInfo(new System.IO.DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory));

        public Task<IFileInfo> PickFileAsync(IEnumerable<string> extensionsFilter = null)
        {

        }

        public abstract Task<IFileInfo[]> PickFilesAsync(IEnumerable<string> extensionsFilter = null)
        {

        }

        public abstract Task<IFileInfo> PickSaveFileAsync(string defaultExtension)
        {

        }

        public abstract Task<IDirectoryInfo> PickDirectoryAsync()
        {

        }

        private IDirectoryInfo GetSpecialFolder(Environment.SpecialFolder specialFolder)
        {
            var path = Environment.GetFolderPath(specialFolder);
            var folder = new System.IO.DirectoryInfo(path);
            return new DirectoryInfo(folder);
        }
    }
}