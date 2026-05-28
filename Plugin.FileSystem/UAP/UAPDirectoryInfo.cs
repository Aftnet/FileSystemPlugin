#if WINDOWS10_0_17763_0_OR_GREATER

using Plugin.FileSystem.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Search;

namespace Plugin.FileSystem
{
    public class UAPDirectoryInfo : NativeItemWrapper<StorageFolder>, IDirectoryInfo
    {
        public UAPDirectoryInfo(StorageFolder nativeItem) : base(nativeItem)
        {
        }

        public string Name => NativeItem.Name;

        public string FullName => NativeItem.Path;

        public Task RenameAsync(string name)
        {
            return NativeItem.RenameAsync(name).AsTask();
        }

        public async Task<IDirectoryInfo> CreateDirectoryAsync(string name)
        {
            var newFolder = await NativeItem.CreateFolderAsync(name);
            return new UAPDirectoryInfo(newFolder);
        }

        public async Task<IFileInfo> CreateFileAsync(string name)
        {
            var newFile = await NativeItem.CreateFileAsync(name);
            return new UAPFileInfo(newFile);
        }

        public Task DeleteAsync()
        {
            return NativeItem.DeleteAsync().AsTask();
        }

        public async Task<IEnumerable<IDirectoryInfo>> EnumerateDirectoriesAsync()
        {
            var folders = await NativeItem.GetFoldersAsync(CommonFolderQuery.DefaultQuery);
            return folders.Select(d => new UAPDirectoryInfo(d)).ToArray();
        }

        public async Task<IDirectoryInfo> GetDirectoryAsync(string name)
        {
            try
            {
                var folder = await NativeItem.GetFolderAsync(name);
                return new UAPDirectoryInfo(folder);
            }
            catch (FileNotFoundException)
            {
                return null;
            }
        }

        public async Task<IEnumerable<IFileInfo>> EnumerateFilesAsync()
        {
            var files = await NativeItem.GetFilesAsync(CommonFileQuery.DefaultQuery);
            return files.Select(d => new UAPFileInfo(d)).ToArray();
        }

        public async Task<IFileInfo> GetFileAsync(string name)
        {
            try
            {
                var file = await NativeItem.GetFileAsync(name);
                return new UAPFileInfo(file);
            }
            catch (FileNotFoundException)
            {
                return null;
            }
        }

        public async Task<IEnumerable<IFileSystemInfo>> EnumerateItemsAsync()
        {
            var folders = await EnumerateDirectoriesAsync();
            var files = await EnumerateFilesAsync();
            var output = folders.Cast<IFileSystemInfo>().Concat(files.Cast<IFileSystemInfo>()).ToArray();
            return output;
        }

        public async Task<DateTimeOffset> GetLastModifiedAsync()
        {
            var properties = await NativeItem.GetBasicPropertiesAsync();
            return properties.DateModified;
        }

        public async Task<IDirectoryInfo> GetParentAsync()
        {
            var parent = await NativeItem.GetParentAsync();
            return new UAPDirectoryInfo(parent);
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

#endif