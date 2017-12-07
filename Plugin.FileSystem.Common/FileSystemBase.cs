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

        public virtual Task<IFileInfo> PickFileAsync(IEnumerable<string> extensionsFilter = null)
        {
            return Task.FromResult(default(IFileInfo));
        }

        public virtual Task<IFileInfo[]> PickFilesAsync(IEnumerable<string> extensionsFilter = null)
        {
            return Task.FromResult(default(IFileInfo[]));
        }

        public virtual Task<IFileInfo> PickSaveFileAsync(string defaultExtension)
        {
            return Task.FromResult(default(IFileInfo));
        }

        public virtual Task<IDirectoryInfo> PickDirectoryAsync()
        {
            return Task.FromResult(default(IDirectoryInfo));
        }

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
