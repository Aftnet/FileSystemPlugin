using Plugin.FileSystem.Abstractions;
using System;
using System.IO;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.Storage;

namespace Plugin.FileSystem
{
    /// <summary>
    /// Implementation of <see cref="IFileSystem"/> over WinRT Storage APIs
    /// </summary>
    internal class FileSystem : IFileSystem
    {
        private readonly ApplicationData ApplicationData = ApplicationData.Current;

        public IDirectoryInfo LocalStorage => new DirectoryInfo(ApplicationData.LocalFolder);

        public IDirectoryInfo RoamingStorage => new DirectoryInfo(ApplicationData.RoamingFolder);

        public IDirectoryInfo InstallLocation => new DirectoryInfo(Package.Current.InstalledLocation);

        public async Task<IFileInfo> GetFileFromPathAsync(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentException();
            }

            StorageFile storageFile;
            try
            {
                storageFile = await StorageFile.GetFileFromPathAsync(path).AsTask().ConfigureAwait(false);
            }
            catch (FileNotFoundException)
            {
                return null;
            }

            return new FileInfo(storageFile);
        }

        public async Task<IDirectoryInfo> GetFolderFromPathAsync(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentException();
            }

            StorageFolder storageFolder;
            try
            {
                storageFolder = await StorageFolder.GetFolderFromPathAsync(path);
            }
            catch (FileNotFoundException)
            {
                return null;
            }

            return new DirectoryInfo(storageFolder);
        }
    }
}
