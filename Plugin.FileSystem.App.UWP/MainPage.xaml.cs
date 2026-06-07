using Plugin.FileSystem.Abstractions;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using WinRT.Interop;

namespace Plugin.FileSystem.App.UWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a <see cref="Frame">.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        readonly IFileSystem fs;

        public MainPage()
        {
            fs = new FileSystem();
            InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var picked = await fs.PickFileAsync();
            if (picked != null)
            {
                FileNameLabel.Text = picked.FullName;
            }
        }
    }
}
