using Plugin.FileSystem.Abstractions;

namespace Plugin.FileSystem
{
    public class FileInfo : NativeItemWrapper<System.IO.FileInfo>, IFileInfo
    {
        public FileInfo(System.IO.FileInfo nativeItem) : base(nativeItem)
        {
        }

        public string Name => NativeItem.Name;

        public string FullName => NativeItem.FullName;

        public Task RenameAsync(string name)
        {
            var newPath = Path.Combine(NativeItem.Directory!.FullName, name);
            return Task.Run(() => NativeItem.MoveTo(newPath));
        }

        public Task<IFileInfo> CopyToAsync(IDirectoryInfo destFolder, bool overwrite = true)
        {
            return CopyToAsync(destFolder, Name, overwrite);
        }

        public async Task<IFileInfo> CopyToAsync(IDirectoryInfo destFolder, string destFileName, bool overwrite = true)
        {
            var newPath = Path.Combine(destFolder.FullName, destFileName);
            var newFile = await Task.Run(() => NativeItem.CopyTo(newPath, overwrite));
            return new FileInfo(newFile);
        }

        public Task DeleteAsync()
        {
            return Task.Run(() => NativeItem.Delete());
        }

        public Task<DateTimeOffset> GetLastModifiedAsync()
        {
            return Task.Run(() =>
            {
                return new DateTimeOffset(NativeItem.LastAccessTimeUtc, TimeSpan.Zero);
            });    
        }

        public Task<ulong> GetLengthAsync()
        {
            return Task.Run(() => (ulong)NativeItem.Length);
        }

        public Task<IDirectoryInfo?> GetParentAsync()
        {
            return Task.Run(() =>
            {
                return NativeItem.Directory != null ? new DirectoryInfo(NativeItem.Directory) as IDirectoryInfo : null;
            });
        }

        public Task MoveToAsync(IDirectoryInfo destFolder, bool overwrite = true)
        {
            return MoveToAsync(destFolder, Name, overwrite);
        }

        public Task MoveToAsync(IDirectoryInfo destFolder, string destFileName, bool overwrite = true)
        {
            var newPath = Path.Combine(destFolder.FullName, destFileName);
            return Task.Run(() => NativeItem.MoveTo(newPath));
        }

        public Task<Stream> OpenAsync(FileAccess access)
        {
            return Task.Run(() => NativeItem.Open(FileMode.OpenOrCreate, access, FileShare.Read) as Stream);
        }

        public override bool Equals(object? obj)
        {
            var other = obj as FileInfo;
            if (other == null)
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
