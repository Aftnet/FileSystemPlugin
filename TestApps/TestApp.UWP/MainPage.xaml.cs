using Plugin.FileSystem;
using Plugin.FileSystem.Abstractions;
using System.Collections.Generic;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace TestApp.UWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private static readonly string[] FilterExt = { ".txt" };

        private IFileSystem FS => CrossFileSystem.Current;

        public MainPage()
        {
            this.InitializeComponent();
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
