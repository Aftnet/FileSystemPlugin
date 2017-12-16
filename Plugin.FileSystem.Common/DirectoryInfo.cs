using Plugin.FileSystem.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
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

        public Task RenameAsync(string name)
        {
            var newPath = Path.Combine(NativeItem.Parent.FullName, name);
            return Task.Run(() => NativeItem.MoveTo(newPath));
        }

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
            return Task.Run(() => NativeItem.Delete(true));
        }

        public async Task<IEnumerable<IDirectoryInfo>> EnumerateDirectoriesAsync()
        {
            var folders = await Task.Run(() => NativeItem.GetDirectories());
            var output = folders.Select(d => new DirectoryInfo(d));
            return output;
        }

        public async Task<IDirectoryInfo> GetDirectoryAsync(string name)
        {
            var folders = await EnumerateDirectoriesAsync();
            return folders.FirstOrDefault(d => d.Name == name);
        }

        public async Task<IEnumerable<IFileInfo>> EnumerateFilesAsync()
        {
            var files = await Task.Run(() => NativeItem.GetFiles());
            var output = files.Select(d => new FileInfo(d));
            return output;
        }

        public async Task<IFileInfo> GetFileAsync(string name)
        {
            var files = await EnumerateFilesAsync();
            return files.FirstOrDefault(d => d.Name == name);
        }

        public async Task<IEnumerable<IFileSystemInfo>> EnumerateItemsAsync()
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

        public override bool Equals(object obj)
        {
            var other = obj as DirectoryInfo;
            if (obj == null)
                return false;

            return FullName == other.FullName;
        }

        public override int GetHashCode()
        {
            return FullName.GetHashCode();
        }

        public override string ToString()
        {
            return FullName;
        }
    }
}
