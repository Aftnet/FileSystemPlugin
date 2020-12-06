using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Foundation;
using Plugin.FileSystem.Abstractions;
using AppKit;
using System.Linq;

namespace Plugin.FileSystem
{
    internal class FileSystem : FileSystemBase
    {
        private const int UIThreadRecoveryTimeMs = 100;
        private static readonly nint OkResponseCode = 1;

        public override IDirectoryInfo LocalStorage => GetSpecialFolder(Environment.SpecialFolder.LocalApplicationData);

        public override IDirectoryInfo RoamingStorage => LocalStorage;

        public override IDirectoryInfo InstallLocation => new DirectoryInfo(new System.IO.DirectoryInfo(NSBundle.MainBundle.BundlePath));

        public override async Task<IFileInfo> PickFileAsync(IEnumerable<string> extensionsFilter = null)
        {
            var output = default(IFileInfo);

            var paths = await GetPathsFromOpenFileDialog(false, false, extensionsFilter);
            var path = paths?.FirstOrDefault();
            if (path != null)
            {
                output = new FileInfo(new System.IO.FileInfo(path));
            }

            return output;
        }

        public override async Task<IFileInfo[]> PickFilesAsync(IEnumerable<string> extensionsFilter = null)
        {
            var output = default(IFileInfo[]);

            var paths = await GetPathsFromOpenFileDialog(true, false, extensionsFilter);
            output = paths?.Select(d => new FileInfo(new System.IO.FileInfo(d))).ToArray(); 

            return output;
        }

        public override async Task<IFileInfo> PickSaveFileAsync(string defaultExtension, string suggestedName = null)
        {
            var panel = NSSavePanel.SavePanel;
            if (!string.IsNullOrEmpty(suggestedName) || string.IsNullOrWhiteSpace(suggestedName))
            {
                panel.NameFieldStringValue = suggestedName;
            }

            panel.AllowedFileTypes = new string[] { defaultExtension.Substring(1) };
            panel.AllowsOtherFileTypes = false;

            var modalResult = panel.RunModal();
            await Task.Delay(UIThreadRecoveryTimeMs);
            if (modalResult != OkResponseCode)
            {
                return null;
            }

            var path = panel.Url?.Path;
            if (path == null)
            {
                return null;
            }

            var file = new System.IO.FileInfo(path);
            return new FileInfo(file);
        }

        public override async Task<IDirectoryInfo> PickDirectoryAsync()
        {
            var output = default(IDirectoryInfo);

            var paths = await GetPathsFromOpenFileDialog(false, true, null);
            var path = paths?.FirstOrDefault();
            if (path != null)
            {
                output = new DirectoryInfo(new System.IO.DirectoryInfo(path));
            }

            return output;
        }

        private IDirectoryInfo GetSpecialFolder(Environment.SpecialFolder specialFolder)
        {
            var path = Environment.GetFolderPath(specialFolder);
            var folder = new System.IO.DirectoryInfo(path);
            return new DirectoryInfo(folder);
        }

        private static async Task<string[]> GetPathsFromOpenFileDialog(bool allowMultipleSelection, bool openFolder, IEnumerable<string> extensionsFilter)
        {
            var panel = NSOpenPanel.OpenPanel;
            panel.AllowsMultipleSelection = allowMultipleSelection;
            panel.CanChooseFiles = !openFolder;
            panel.CanChooseDirectories = openFolder;

            if (extensionsFilter != null && extensionsFilter.Any())
            {
                panel.AllowedFileTypes = extensionsFilter.Select(d => d.Substring(1)).ToArray();
            }

            var modalResult = panel.RunModal();
            await Task.Delay(UIThreadRecoveryTimeMs);
            if (modalResult != OkResponseCode)
            {
                return null;
            }

            var output = panel.Urls?.Select(d => d.Path).ToArray();
            return output;
        }
    }
}