using Plugin.FileSystem.Abstractions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Plugin.FileSystem
{
    internal abstract class FileSystemBase : IFileSystem
    {
        public abstract IDirectoryInfo LocalStorage { get; }

        public abstract IDirectoryInfo RoamingStorage { get; }

        public abstract IDirectoryInfo InstallLocation { get; }

        public abstract Task<IFileInfo> PickFileAsync(IEnumerable<string> extensionsFilter = null);

        public abstract Task<IFileInfo[]> PickFilesAsync(IEnumerable<string> extensionsFilter = null);

        public abstract Task<IFileInfo> PickSaveFileAsync(string defaultExtension);

        public abstract Task<IDirectoryInfo> PickDirectoryAsync();

        public Task<IFileInfo> GetFileFromPathAsync(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentException();
            }

            var file = new System.IO.FileInfo(path);
            IFileInfo output = file.Exists ? new FileInfo(file) : null;
            return Task.FromResult(output);
        }

        public Task<IDirectoryInfo> GetDirectoryFromPathAsync(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentException();
            }

            var folder = new System.IO.DirectoryInfo(path);
            IDirectoryInfo output = folder.Exists ? new DirectoryInfo(folder) : null;
            return Task.FromResult(output);
        }
    }
}
