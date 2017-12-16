using System.Collections.Generic;
using System.Threading.Tasks;

namespace Plugin.FileSystem.Abstractions
{
    /// <summary>
    /// Represents a file system folder
    /// </summary>
    public interface IDirectoryInfo : IFileSystemInfo
    {
        Task<IFileInfo> CreateFileAsync(string name);

        Task<IDirectoryInfo> CreateDirectoryAsync(string name);
        
        Task<IEnumerable<IDirectoryInfo>> EnumerateDirectoriesAsync();

        Task<IDirectoryInfo> GetDirectoryAsync(string name);
        
        Task<IEnumerable<IFileInfo>> EnumerateFilesAsync();

        Task<IFileInfo> GetFileAsync(string name);

        Task<IEnumerable<IFileSystemInfo>> EnumerateItemsAsync();
    }
}
