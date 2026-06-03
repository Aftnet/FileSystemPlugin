using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Plugin.FileSystem.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Plugin.FileSystem.App.WinUI
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        readonly IFileSystem fs;

        public MainWindow()
        {
            InitializeComponent();
            fs = new FileSystem(this);
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
