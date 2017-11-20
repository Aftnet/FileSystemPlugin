using System;

using AppKit;
using Foundation;
using Plugin.FileSystem;

namespace TestApp.macOS
{
    public partial class ViewController : NSViewController
    {
        private readonly Shared.EventHandler Handler = new Shared.EventHandler(CrossFileSystem.Current);

        public ViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            // Do any additional setup after loading the view.

        }

        public override NSObject RepresentedObject
        {
            get
            {
                return base.RepresentedObject;
            }
            set
            {
                base.RepresentedObject = value;
                // Update the view, if already loaded.
            }
        }

        partial void OpenFileClicked(Foundation.NSObject sender)
        {
            Handler.OpenFile();
        }

        partial void OpenFileTxtClicked(Foundation.NSObject sender)
        {
            Handler.OpenFileTxt();
        }

        partial void OpenFilesClicked(Foundation.NSObject sender)
        {
            Handler.OpenFiles();
        }

        partial void OpenFilesTxtClicked(Foundation.NSObject sender)
        {
            Handler.OpenFilesTxt();
        }

        partial void OpenFolderClicked(Foundation.NSObject sender)
        {
            Handler.OpenFolder();
        }

        partial void SaveFileClicked(Foundation.NSObject sender)
        {
            Handler.SaveFile();
        }
    }
}
