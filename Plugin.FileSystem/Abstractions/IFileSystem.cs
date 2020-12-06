using System.Collections.Generic;
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

        Task<IFileInfo> PickFileAsync(IEnumerable<string> extensionsFilter = null);

        Task<IFileInfo[]> PickFilesAsync(IEnumerable<string> extensionsFilter = null);

        Task<IFileInfo> PickSaveFileAsync(string defaultExtension, string suggestedName = null);

        Task<IDirectoryInfo> PickDirectoryAsync();

        Task<IFileInfo> GetFileFromPathAsync(string path);

        Task<IDirectoryInfo> GetDirectoryFromPathAsync(string path);
    }
}
