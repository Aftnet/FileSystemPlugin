using Plugin.FileSystem.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Plugin.FileSystem
{
    internal class FileSystem : FileSystemBase
    {
        private const string DefaultFilter = "*.*";

        public override IDirectoryInfo LocalStorage => GetSpecialFolder(Environment.SpecialFolder.LocalApplicationData);

        public override IDirectoryInfo RoamingStorage => GetSpecialFolder(Environment.SpecialFolder.ApplicationData);

        public override IDirectoryInfo InstallLocation => new DirectoryInfo(new System.IO.DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory));

        public override Task<IFileInfo> PickFileAsync(IEnumerable<string> extensionsFilter = null)
        {
            var dialog = new OpenFileDialog();
            dialog.Filter = GenerateFilterString(extensionsFilter);
            dialog.Multiselect = false;
            dialog.CheckFileExists = true;
            dialog.CheckPathExists = true;
            var result = dialog.ShowDialog();
            if (result != DialogResult.OK)
            {
                return Task.FromResult(default(IFileInfo));
            }

            IFileInfo output = new FileInfo(new System.IO.FileInfo(dialog.FileName));
            return Task.FromResult(output);
        }

        public override Task<IFileInfo[]> PickFilesAsync(IEnumerable<string> extensionsFilter = null)
        {
            var dialog = new OpenFileDialog();
            dialog.Filter = GenerateFilterString(extensionsFilter);
            dialog.Multiselect = true;
            dialog.CheckFileExists = true;
            dialog.CheckPathExists = true;
            var result = dialog.ShowDialog();
            if (result != DialogResult.OK)
            {
                return Task.FromResult(default(IFileInfo[]));
            }

            var output = dialog.FileNames.Select(d => new FileInfo(new System.IO.FileInfo(d)) as IFileInfo).ToArray();
            return Task.FromResult(output);
        }

        public override Task<IFileInfo> PickSaveFileAsync(string defaultExtension, string suggestedName = null)
        {
            var dialog = new SaveFileDialog();
            if (!string.IsNullOrEmpty(suggestedName) || string.IsNullOrWhiteSpace(suggestedName))
            {
                dialog.FileName = suggestedName;
            }
            dialog.Filter = $"File | *{defaultExtension}";
            dialog.DefaultExt = dialog.Filter;

            var result = dialog.ShowDialog();
            if (result != DialogResult.OK)
            {
                return Task.FromResult(default(IFileInfo));
            }

            IFileInfo output = new FileInfo(new System.IO.FileInfo(dialog.FileName));
            return Task.FromResult(output);
        }

        public override Task<IDirectoryInfo> PickDirectoryAsync()
        {
            var dialog = new FolderBrowserDialog();
            dialog.ShowNewFolderButton = true;
            var result = dialog.ShowDialog();
            if (result != DialogResult.OK)
            {
                return Task.FromResult(default(IDirectoryInfo));
            }

            return GetDirectoryFromPathAsync(dialog.SelectedPath);
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