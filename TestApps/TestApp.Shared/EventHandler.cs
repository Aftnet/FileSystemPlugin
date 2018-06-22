using Plugin.FileSystem.Abstractions;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace TestApp.Shared
{
    public class EventHandler
    {
        private static readonly string[] FilterExt = { ".txt", ".docx" };

        private readonly IFileSystem FileSystem;

        public EventHandler(IFileSystem fileSystem)
        {
            FileSystem = fileSystem;
        }

        public void OpenFile()
        {
            OpenFileHandler(null);
        }

        public void OpenFileTxt()
        {
            OpenFileHandler(FilterExt);
        }

        private async void OpenFileHandler(IEnumerable<string> extensions)
        {
            var file = await FileSystem.PickFileAsync(extensions);
            if (file == null)
                return;

            using (var stream = await file.OpenAsync(System.IO.FileAccess.Read))
            {
                Debug.WriteLine($"Stream length {stream.Length}");
            }
        }

        public void OpenFiles()
        {
            OpenFilesHandler(null);
        }

        public void OpenFilesTxt()
        {
            OpenFilesHandler(FilterExt);
        }

        private async void OpenFilesHandler(IEnumerable<string> extensions)
        {
            var files = await FileSystem.PickFilesAsync(extensions);
            if (files == null)
                return;

            var file = files.FirstOrDefault();
            if (file == null)
                return;

            using (var stream = await file.OpenAsync(System.IO.FileAccess.Read))
            {

            }
        }

        public async void OpenFolder()
        {
            var folder = await FileSystem.PickDirectoryAsync();
            if (folder == null)
                return;

            var items = await folder.EnumerateItemsAsync();
            var count = items.Count();
        }

        public async void SaveFile()
        {
            var file = await FileSystem.PickSaveFileAsync(".ext");
            if (file == null)
                return;

            using (var stream = await file.OpenAsync(System.IO.FileAccess.ReadWrite))
            {

            }
        }
    }
}
