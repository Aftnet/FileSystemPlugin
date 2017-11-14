using Plugin.FileSystem.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Plugin.FileSystem
{
    public class DirectoryInfo : NativeItemWrapper<System.IO.DirectoryInfo>, IDirectoryInfo
    {
        public DirectoryInfo(System.IO.DirectoryInfo nativeItem) : base(nativeItem)
        {
        }

        public string Name => NativeItem.Name;

        public string FullName => NativeItem.FullName;

        public async Task<IFileInfo> CreateFileAsync(string name)
        {
            var file = await Task.Run(() =>
            {
                var fullPath = System.IO.Path.Combine(NativeItem.FullName, name);
                var newFile = new System.IO.FileInfo(fullPath);
                using (var stream = newFile.Create())
                {

                }

                return newFile;
            });

            return new FileInfo(file);
        }

        public async Task<IDirectoryInfo> CreateDirectoryAsync(string name)
        {
            var newFolder = await Task.Run(() => NativeItem.CreateSubdirectory(name));
            return new DirectoryInfo(newFolder);
        }

        public Task DeleteAsync()
        {
            return Task.Run(() => NativeItem.Delete());
        }

        public async Task<IEnumerable<IDirectoryInfo>> EnumerateDirectoriesAsync()
        {
            var folders = await Task.Run(() => NativeItem.GetDirectories());
            var output = folders.Select(d => new DirectoryInfo(d));
            return output;
        }

        public async Task<IEnumerable<IFileInfo>> EnumerateFilesAsync()
        {
            var folders = await Task.Run(() => NativeItem.GetFiles());
            var output = folders.Select(d => new FileInfo(d));
            return output;
        }

        public async Task<IEnumerable<IFileSystemInfo>> EnumerateFileItemsAsync()
        {
            var folders = await EnumerateDirectoriesAsync();
            var files = await EnumerateFilesAsync();
            var output = folders.Cast<IFileSystemInfo>().Concat(files.Cast<IFileSystemInfo>()).ToArray();
            return output;
        }

        public Task<DateTimeOffset> GetLastModifiedAsync()
        {
            var modifiedTime = new DateTimeOffset(NativeItem.LastWriteTimeUtc, TimeSpan.Zero);
            return Task.FromResult(modifiedTime);
        }

        public Task<IDirectoryInfo> GetParentAsync()
        {
            var output = new DirectoryInfo(NativeItem.Parent);
            return Task.FromResult(output as IDirectoryInfo);
        }
    }
}
