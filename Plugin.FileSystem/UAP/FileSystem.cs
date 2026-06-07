#if WINDOWS10_0_17763_0_OR_GREATER

using Plugin.FileSystem.Abstractions;
using Windows.ApplicationModel;
using Windows.Storage;
using Windows.Storage.AccessCache;
using Windows.Storage.Pickers;
using Windows.Storage.Pickers.Provider;
using WinRT;
using WinRT.Interop;

namespace Plugin.FileSystem
{
    /// <summary>
    /// Implementation of <see cref="IFileSystem"/> over WinRT Storage APIs
    /// </summary>
    public class FileSystem : IFileSystem
    {
        private readonly nint? WindowHandle;

        private const uint FutureAccessListMaxEntries = 10;
        private uint FutureAccessListCounter = 0;

        private const string DefaultExtensionFilter = "*";

        private readonly ApplicationData ApplicationData = ApplicationData.Current;

        public IDirectoryInfo LocalStorage => new UAPDirectoryInfo(ApplicationData.LocalFolder);

        public IDirectoryInfo RoamingStorage => new UAPDirectoryInfo(ApplicationData.RoamingFolder);

        public IDirectoryInfo InstallLocation => new UAPDirectoryInfo(Package.Current.InstalledLocation);

        public FileSystem()
        {
        }

        public FileSystem(nint windowHandle)
        {
            WindowHandle = windowHandle;
        }

        public async Task<IFileInfo?> PickFileAsync(IEnumerable<string>? extensionsFilter = null)
        {
            var picker = new FileOpenPicker();
            if (WindowHandle is nint handle)
            {
                InitializeWithWindow.Initialize(picker, handle);
            }

            if (extensionsFilter == null)
            {
                extensionsFilter = Enumerable.Empty<string>();
            }

            GenerateExtensionFilterForPicker(picker.FileTypeFilter, extensionsFilter);

            var file = await picker.PickSingleFileAsync();
            return file != null ? new UAPFileInfo(file) : null;
        }

        public async Task<IFileInfo[]?> PickFilesAsync(IEnumerable<string>? extensionsFilter = null)
        {
            var picker = new FileOpenPicker();
            if (WindowHandle is nint handle)
            {
                InitializeWithWindow.Initialize(picker, handle);
            }

            if (extensionsFilter == null)
            {
                extensionsFilter = Enumerable.Empty<string>();
            }

            GenerateExtensionFilterForPicker(picker.FileTypeFilter, extensionsFilter);

            var files = await picker.PickMultipleFilesAsync();
            var output = files != null ? files.Select(d => new UAPFileInfo(d)).ToArray() : null;
            return output;
        }

        public async Task<IFileInfo?> PickSaveFileAsync(string defaultExtension, string? suggestedName = null)
        {
            var picker = new FileSavePicker();
            if (WindowHandle is nint handle)
            {
                InitializeWithWindow.Initialize(picker, handle);
            }

            if (!string.IsNullOrEmpty(suggestedName) || string.IsNullOrWhiteSpace(suggestedName))
            {
                picker.SuggestedFileName = suggestedName;
            }

            picker.FileTypeChoices.Add("File", new List<string> { defaultExtension });

            var file = await picker.PickSaveFileAsync();
            return file != null ? new UAPFileInfo(file) : null;
        }


        public async Task<IDirectoryInfo?> PickDirectoryAsync()
        {
            var picker = new FolderPicker();
            if (WindowHandle is nint handle)
            {
                InitializeWithWindow.Initialize(picker, handle);
            }

            picker.FileTypeFilter.Add(DefaultExtensionFilter);

            var folder = await picker.PickSingleFolderAsync();
            if (folder == null)
            {
                return null;
            }

            var folderId = GenerateFutureAccessListId();
            StorageApplicationPermissions.FutureAccessList.AddOrReplace(folderId, folder);
            return new UAPDirectoryInfo(folder);
        }

        public async Task<IFileInfo?> GetFileFromPathAsync(string path)
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

            return new UAPFileInfo(storageFile);
        }

        public async Task<IDirectoryInfo?> GetDirectoryFromPathAsync(string path)
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

            return new UAPDirectoryInfo(storageFolder);
        }

        private static void GenerateExtensionFilterForPicker(IList<string> pickerFilter, IEnumerable<string> inputFilter)
        {
            if (inputFilter != null && inputFilter.Any())
            {
                foreach (var i in inputFilter)
                {
                    pickerFilter.Add(i);
                }
            }
            else
            {
                pickerFilter.Add(DefaultExtensionFilter);
            }
        }

        private string GenerateFutureAccessListId()
        {
            FutureAccessListCounter = (++FutureAccessListCounter) % FutureAccessListMaxEntries;
            return $"FutureAccessList{FutureAccessListCounter}";
        }
    }
}

#endif