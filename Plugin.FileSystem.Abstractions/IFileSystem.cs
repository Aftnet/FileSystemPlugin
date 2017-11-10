using System.Threading.Tasks;

namespace Plugin.FileSystem.Abstractions
{
    /// <summary>
    /// Represents a file system.
    /// </summary>
    public interface IFileSystem
    {
        IDirectoryInfo LocalStorage { get; }

        IDirectoryInfo RoamingStorage { get; }

        IDirectoryInfo InstallLocation { get; }

        Task<IFileInfo> GetFileFromPathAsync(string path);

        Task<IDirectoryInfo> GetFolderFromPathAsync(string path);
    }
}
