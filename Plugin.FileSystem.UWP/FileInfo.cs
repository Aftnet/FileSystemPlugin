using Plugin.FileSystem.Abstractions;
using System;
using System.IO;
using System.Threading.Tasks;
using Windows.Storage;

namespace Plugin.FileSystem
{
    public class FileInfo : NativeItemWrapper<StorageFile>, IFileInfo
    {
        public FileInfo(StorageFile nativeItem) : base(nativeItem)
        {
        }

        public string Name => NativeItem.Name;

        public string FullName => NativeItem.Path;

        public async Task<IFileInfo> CopyToAsync(IDirectoryInfo destFolder, string destFileName, bool overwrite = true)
        {
            var nativeFolder = (destFolder as NativeItemWrapper<StorageFolder>).NativeItem;
            var newFile = await NativeItem.CopyAsync(nativeFolder, destFileName, overwrite ? NameCollisionOption.ReplaceExisting : NameCollisionOption.FailIfExists);
            return new FileInfo(newFile);
        }

        public Task DeleteAsync()
        {
            return NativeItem.DeleteAsync().AsTask();
        }

        public async Task<DateTimeOffset> GetLastModifiedAsync()
        {
            var properties = await NativeItem.GetBasicPropertiesAsync();
            return properties.DateModified;
        }

        public async Task<ulong> GetLengthAsync()
        {
            var properties = await NativeItem.GetBasicPropertiesAsync();
            return properties.Size;
        }

        public async Task<IDirectoryInfo> GetParentAsync()
        {
            var parent = await NativeItem.GetParentAsync();
            return new DirectoryInfo(parent);
        }

        public Task MoveToAsync(IDirectoryInfo destFolder, bool overwrite = true)
        {
            return MoveToAsync(destFolder, Name, overwrite);
        }

        public Task MoveToAsync(IDirectoryInfo destFolder, string destFileName, bool overwrite = true)
        {
            var nativeFolder = (destFolder as NativeItemWrapper<StorageFolder>).NativeItem;
            return NativeItem.MoveAsync(nativeFolder, destFileName, overwrite ? NameCollisionOption.ReplaceExisting : NameCollisionOption.FailIfExists).AsTask();
        }

        public async Task<Stream> OpenAsync(FileAccess access)
        {
            var stream = await NativeItem.OpenAsync(access == FileAccess.Read ? FileAccessMode.Read : FileAccessMode.ReadWrite);
            return stream.AsStream();
        }
    }
}
