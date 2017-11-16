using Plugin.FileSystem;
using Plugin.FileSystem.Abstractions;
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
        private IFileSystem FS => CrossFileSystem.Current;

        public MainPage()
        {
            this.InitializeComponent();
        }

        private async void OpenFile_Click(object sender, RoutedEventArgs e)
        {
            var file = await FS.PickFileAsync();
            if (file == null)
                return;

            using (var stream = await file.OpenAsync(System.IO.FileAccess.Read))
            {

            }
        }

        private async void OpenFiles_Click(object sender, RoutedEventArgs e)
        {
            var files = await FS.PickFilesAsync();
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
