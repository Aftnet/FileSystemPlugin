using Plugin.FileSystem;
using Plugin.FileSystem.Abstractions;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace TestApp.Net46
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static readonly string[] FilterExt = { ".txt", ".docx" };

        private IFileSystem FS => CrossFileSystem.Current;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void OpenFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileHandler(null);
        }

        private void OpenFileTxt_Click(object sender, RoutedEventArgs e)
        {
            OpenFileHandler(FilterExt);
        }

        private async void OpenFileHandler(IEnumerable<string> extensions)
        {
            var file = await FS.PickFileAsync(extensions);
            if (file == null)
                return;

            using (var stream = await file.OpenAsync(System.IO.FileAccess.Read))
            {

            }
        }

        private void OpenFiles_Click(object sender, RoutedEventArgs e)
        {
            OpenFilesHandler(null);
        }

        private void OpenFilesTxt_Click(object sender, RoutedEventArgs e)
        {
            OpenFilesHandler(FilterExt);
        }

        private async void OpenFilesHandler(IEnumerable<string> extensions)
        {
            var files = await FS.PickFilesAsync(extensions);
            if (files == null)
                return;

            var file = files.FirstOrDefault();
            if (file == null)
                return;

            using (var stream = await file.OpenAsync(System.IO.FileAccess.Read))
            {

            }
        }

        private async void OpenFolder_Click(object sender, RoutedEventArgs e)
        {
            var folder = await FS.PickDirectoryAsync();
            if (folder == null)
                return;

            var items = await folder.EnumerateItemsAsync();
            var count = items.Count();
        }

        private async void SaveFile_Click(object sender, RoutedEventArgs e)
        {
            var file = await FS.PickSaveFileAsync(".lol");
            if (file == null)
                return;

            using (var stream = await file.OpenAsync(System.IO.FileAccess.ReadWrite))
            {

            }
        }
    }
}
