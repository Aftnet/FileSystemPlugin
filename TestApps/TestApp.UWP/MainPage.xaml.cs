using Plugin.FileSystem;
using TestApp.Shared;
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
        private readonly EventHandler Handler = new EventHandler(CrossFileSystem.Current);

        public MainPage()
        {
            this.InitializeComponent();
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
