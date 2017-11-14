using Plugin.FileSystem.Abstractions;
using System;
using System.Threading.Tasks;

namespace Plugin.FileSystem
{
    internal abstract class FileSystemBase : IFileSystem
    {
        public abstract IDirectoryInfo LocalStorage { get; }

        public abstract IDirectoryInfo RoamingStorage { get; }

        public abstract IDirectoryInfo InstallLocation { get; }

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
