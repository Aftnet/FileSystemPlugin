using Plugin.FileSystem.Abstractions;

namespace Plugin.FileSystem
{
    internal abstract class FileSystemBase : IFileSystem
    {
        public abstract IDirectoryInfo LocalStorage { get; }

        public abstract IDirectoryInfo RoamingStorage { get; }

        public abstract IDirectoryInfo InstallLocation { get; }

        public virtual Task<IFileInfo?> PickFileAsync(IEnumerable<string>? extensionsFilter = null)
        {
            return Task.FromResult(default(IFileInfo));
        }

        public virtual Task<IFileInfo[]?> PickFilesAsync(IEnumerable<string>? extensionsFilter = null)
        {
            return Task.FromResult(default(IFileInfo[]));
        }

        public virtual Task<IFileInfo?> PickSaveFileAsync(string defaultExtension, string? suggestedName = null)
        {
            return Task.FromResult(default(IFileInfo));
        }

        public virtual Task<IDirectoryInfo?> PickDirectoryAsync()
        {
            return Task.FromResult(default(IDirectoryInfo));
        }

        public Task<IFileInfo?> GetFileFromPathAsync(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentException();
            }

            var output = default(IFileInfo);
            var file = new System.IO.FileInfo(path);
            if (file != null && file.Exists)
            {
                output = new FileInfo(file);
            }

            return Task.FromResult(output);
        }

        public Task<IDirectoryInfo?> GetDirectoryFromPathAsync(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentException();
            }

            var output = default(IDirectoryInfo);
            var folder = new System.IO.DirectoryInfo(path);
            if (folder != null && folder.Exists)
            {
                output = new DirectoryInfo(folder);
            }

            return Task.FromResult(output);
        }
    }
}
