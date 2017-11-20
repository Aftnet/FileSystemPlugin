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
        public override IDirectoryInfo LocalStorage => GetSpecialFolder(Environment.SpecialFolder.LocalApplicationData);

        public override IDirectoryInfo RoamingStorage => GetSpecialFolder(Environment.SpecialFolder.ApplicationData);

        public override IDirectoryInfo InstallLocation => new DirectoryInfo(new System.IO.DirectoryInfo(NSBundle.MainBundle.BundlePath));

        public override Task<IFileInfo> PickFileAsync(IEnumerable<string> extensionsFilter = null)
        {
            var panel = new NSOpenPanel
            {
                AllowedFileTypes = GeneratePanelFilter(extensionsFilter)
            };

            if (panel.RunModal() != 1)
            {
                return Task.FromResult(default(IFileInfo));
            }

            var uri = panel.Urls.FirstOrDefault();
            if (uri == null)
            {
                return Task.FromResult(default(IFileInfo));
            }

            var path = uri.Path;
            var output = new FileInfo(new System.IO.FileInfo(path));
            return Task.FromResult(output as IFileInfo);
        }

        public override Task<IFileInfo[]> PickFilesAsync(IEnumerable<string> extensionsFilter = null)
        {
            return Task.FromResult(default(IFileInfo[]));
        }

        public override Task<IFileInfo> PickSaveFileAsync(string defaultExtension)
        {
            return Task.FromResult(default(IFileInfo));
        }

        public override Task<IDirectoryInfo> PickDirectoryAsync()
        {
            return Task.FromResult(default(IDirectoryInfo));
        }

        private IDirectoryInfo GetSpecialFolder(Environment.SpecialFolder specialFolder)
        {
            var path = Environment.GetFolderPath(specialFolder);
            var folder = new System.IO.DirectoryInfo(path);
            return new DirectoryInfo(folder);
        }

        private static string[] GeneratePanelFilter(IEnumerable<string> extensionsFilter)
        {
            string[] output = null;
            if (extensionsFilter != null && extensionsFilter.Any())
            {
                output = extensionsFilter.Select(d => d.Substring(1)).ToArray();
            }

            return output;
        }
    }
}