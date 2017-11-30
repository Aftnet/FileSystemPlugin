using Plugin.FileSystem;
using System.Windows;
using TestApp.Shared;

namespace TestApp.Net46
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly EventHandler Handler = new EventHandler(CrossFileSystem.Current);

        public MainWindow()
        {
            InitializeComponent();
        }

        private void OpenFile_Click(object sender, RoutedEventArgs e)
        {
            Handler.OpenFile();
        }

        private void OpenFileTxt_Click(object sender, RoutedEventArgs e)
        {
            Handler.OpenFileTxt();
        }

        private void OpenFiles_Click(object sender, RoutedEventArgs e)
        {
            Handler.OpenFiles();
        }

        private void OpenFilesTxt_Click(object sender, RoutedEventArgs e)
        {
            Handler.OpenFilesTxt();
        }

        private void OpenFolder_Click(object sender, RoutedEventArgs e)
        {
            Handler.OpenFolder();
        }

        private void SaveFile_Click(object sender, RoutedEventArgs e)
        {
            Handler.SaveFile();
        }
    }
}
