using System;
using System.Threading.Tasks;

namespace Plugin.Filesystem.Abstractions
{
    public interface IFileSystemInfo
    {
        string Name { get; }

        string FullName { get; }

        Task<DateTimeOffset> GetLastModifiedAsync();

        Task<IDirectoryInfo> GetParentAsync();

        Task DeleteAsync();
    }
}
