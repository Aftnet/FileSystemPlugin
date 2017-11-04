using System.Threading.Tasks;

namespace Plugin.Filesystem.Abstractions
{
    /// <summary>
    /// Represents a file system.
    /// </summary>
    public interface IFileSystem
    {
        IDirectoryInfo LocalStorage { get; }

        IDirectoryInfo RoamingStorage { get; }

        Task<IFileInfo> GetFileFromPathAsync(string path);

        Task<IDirectoryInfo> GetFolderFromPathAsync(string path);
    }
}
