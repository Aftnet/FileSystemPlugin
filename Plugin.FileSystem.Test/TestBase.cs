using Plugin.FileSystem.Abstractions;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Plugin.FileSystem.Test
{
    public abstract class TestBase : IDisposable
    {
        protected readonly IFileSystem FileSystem;
        protected readonly IDirectoryInfo TestRootFolder;

        protected TestBase(IFileSystem fileSystem)
        {
            FileSystem = fileSystem;
            TestRootFolder = fileSystem.LocalStorage.CreateDirectoryAsync("FileSystemPluginTest").Result;
        }

        public void Dispose()
        {
            TestRootFolder.DeleteAsync().Wait();
        }

        [Fact]
        public async Task CreateFolderWorks()
        {
            var folderOne = await TestRootFolder.CreateDirectoryAsync("Test");

            var folders = await TestRootFolder.EnumerateDirectoriesAsync();
            var items = await TestRootFolder.EnumerateItemsAsync();
            Assert.Collection(folders, d => Assert.Equal(folderOne, d));
            Assert.Single(items);
        }

        [Fact]
        public async Task RenameFolderWorks()
        {
            var folderOne = await TestRootFolder.CreateDirectoryAsync("Test");

            string newName = "Renamed";
            await folderOne.RenameAsync(newName);

            var folders = await TestRootFolder.EnumerateDirectoriesAsync();
            Assert.Collection(folders, d => Assert.Equal(newName, d.Name));

            var parent = await folderOne.GetParentAsync();
            Assert.Equal(TestRootFolder, parent);
        }

        [Fact]
        public async Task DeleteFolderWorks()
        {
            var folderOne = await TestRootFolder.CreateDirectoryAsync("Test");
            var folders = await TestRootFolder.EnumerateDirectoriesAsync();
            Assert.Collection(folders, d => Assert.Equal(folderOne, d));

            await folderOne.DeleteAsync();

            folders = await TestRootFolder.EnumerateDirectoriesAsync();
            Assert.Empty(folders);
        }

        [Fact]
        public async Task CreateFileWorks()
        {
            var fileOne = await TestRootFolder.CreateFileAsync("one.ext");

            var files = await TestRootFolder.EnumerateFilesAsync();
            var items = await TestRootFolder.EnumerateItemsAsync();
            Assert.Collection(files, d => Assert.Equal(fileOne, d));
            Assert.Single(items);
        }

        [Fact]
        public async Task RenameFileWorks()
        {
            var fileOne = await TestRootFolder.CreateFileAsync("one.ext");

            string newName = "Renamed.ext";
            await fileOne.RenameAsync(newName);

            var files = await TestRootFolder.EnumerateFilesAsync();
            Assert.Collection(files, d => Assert.Equal(newName, d.Name));

            var parent = await fileOne.GetParentAsync();
            Assert.Equal(TestRootFolder, parent);
        }

        [Fact]
        public async Task DeleteFileWorks()
        {
            var fileOne = await TestRootFolder.CreateFileAsync("one.ext");
            var files = await TestRootFolder.EnumerateFilesAsync();
            Assert.Collection(files, d => Assert.Equal(fileOne, d));

            await fileOne.DeleteAsync();

            files = await TestRootFolder.EnumerateFilesAsync();
            Assert.Empty(files);
        }

        [Fact]
        public async Task CopyFileWorks()
        {
        }

        [Fact]
        public async Task MoveFileWorks()
        {
        }

        [Fact]
        public async Task OpenFileWorks()
        {
        }

        [Fact]
        public async Task FileOperationsWork()
        {
            var testBuffer = new byte[128];
            var random = new Random();
            random.NextBytes(testBuffer);

            var folderOne = await TestRootFolder.CreateDirectoryAsync("TestFolderOne");
            var folderTwo = await TestRootFolder.CreateDirectoryAsync("TestFolderTwo");

            var items = await TestRootFolder.EnumerateItemsAsync();
            Assert.Collection(items, d => Assert.Equal(d, folderOne), d => Assert.Equal(d, folderTwo));

            var file = await folderOne.CreateFileAsync("FileName.ext");
            using (var stream = await file.OpenAsync(System.IO.FileAccess.ReadWrite))
            {
                await stream.WriteAsync(testBuffer, 0, testBuffer.Length);
            }

            var files = await folderOne.EnumerateFilesAsync();
            Assert.Collection(files, d => Assert.Equal(file, d));

            await file.MoveToAsync(folderTwo);
            Assert.StartsWith(folderTwo.FullName, file.FullName);
            files = await folderOne.EnumerateFilesAsync();
            Assert.Empty(files);
            files = await folderTwo.EnumerateFilesAsync();
            Assert.Collection(files, d => Assert.Equal(file, d));
        }

        [Fact]
        public void KnownDirectoriesWork()
        {
            Assert.NotNull(FileSystem.InstallLocation);
            Assert.NotNull(FileSystem.LocalStorage);
            Assert.NotNull(FileSystem.RoamingStorage);
        }
    }
}
