#if !ANDROID && !IOS && !MACOS && !WINDOWS10_0_17763_0_OR_GREATER

using Plugin.FileSystem.Abstractions;

namespace Plugin.FileSystem
{
    internal class FileSystem : FileSystemBase
    {
        private const string DefaultFilter = "*.*";

        public override IDirectoryInfo LocalStorage => GetSpecialFolder(Environment.SpecialFolder.LocalApplicationData);

        public override IDirectoryInfo RoamingStorage => GetSpecialFolder(Environment.SpecialFolder.ApplicationData);

        public override IDirectoryInfo InstallLocation => new DirectoryInfo(new System.IO.DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory));

        public override Task<IFileInfo?> PickFileAsync(IEnumerable<string>? extensionsFilter = null)
        {
            
            return Task.FromResult(default(IFileInfo));
        }

        public override Task<IFileInfo[]?> PickFilesAsync(IEnumerable<string>? extensionsFilter = null)
        {
            
            return Task.FromResult(default(IFileInfo[]));
        }

        public override Task<IFileInfo?> PickSaveFileAsync(string defaultExtension, string? suggestedName = null)
        {
            return Task.FromResult(default(IFileInfo));
        }

        public override Task<IDirectoryInfo?> PickDirectoryAsync()
        {
            return Task.FromResult(default(IDirectoryInfo));
        }

        private IDirectoryInfo GetSpecialFolder(Environment.SpecialFolder specialFolder)
        {
            var path = Environment.GetFolderPath(specialFolder);
            var folder = new System.IO.DirectoryInfo(path);
            return new DirectoryInfo(folder);
        }

        private static string GenerateFilterString(IEnumerable<string> extensionsFilter)
        {
            var filter = DefaultFilter;
            if (extensionsFilter != null && extensionsFilter.Any())
            {
                filter = string.Join(";", extensionsFilter.Select(d => $"*{d}"));
            }

            var output = $"All files | {filter}";
            return output;
        }
    }
}

#endif