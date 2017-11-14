using System.IO;
using System.Threading.Tasks;

namespace Plugin.FileSystem.Abstractions
{
    /// <summary>
    /// Represents a file
    /// </summary>
    public interface IFileInfo : IFileSystemInfo
    {
        Task<ulong> GetLengthAsync();

        Task<IFileInfo> CopyToAsync(IDirectoryInfo destFolder, string destFileName, bool overwrite = true);

        Task MoveToAsync(IDirectoryInfo destFolder, bool overwrite = true);

        Task MoveToAsync(IDirectoryInfo destFolder, string destFileName, bool overwrite = true);

        Task<Stream> OpenAsync(FileAccess access);
    }
}
