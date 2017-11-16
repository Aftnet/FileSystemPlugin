using Plugin.FileSystem;
using Plugin.FileSystem.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TestApp.Net46
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IFileSystem FS => CrossFileSystem.Current;

        public MainWindow()
        {
            InitializeComponent();
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
