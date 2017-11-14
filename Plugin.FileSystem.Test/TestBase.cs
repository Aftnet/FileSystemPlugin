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
        public async Task FileOperationsWork()
        {
            var testBuffer = new byte[128];
            var random = new Random();
            random.NextBytes(testBuffer);

            var folderOne = await TestRootFolder.CreateDirectoryAsync("TestFolderOne");
            var folderTwo = await TestRootFolder.CreateDirectoryAsync("TestFolderTwo");

            var items = await TestRootFolder.EnumerateFileItemsAsync();
            Assert.Collection(items, d => Assert.Equal(d.FullName, folderOne.FullName), d => Assert.Equal(d.FullName, folderTwo.FullName));

            var file = await folderOne.CreateFileAsync("FileName.ext");
            using (var stream = await file.OpenAsync(System.IO.FileAccess.ReadWrite))
            {
                await stream.WriteAsync(testBuffer, 0, testBuffer.Length);
            }

            var files = await folderOne.EnumerateFilesAsync();
            Assert.Collection(files, d => Assert.Equal(file.FullName, d.FullName));

            await file.MoveToAsync(folderTwo);
            Assert.StartsWith(folderTwo.FullName, file.FullName);
            files = await folderOne.EnumerateFilesAsync();
            Assert.Empty(files);
            files = await folderTwo.EnumerateFilesAsync();
            Assert.Collection(files, d => Assert.Equal(file.FullName, d.FullName));
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
