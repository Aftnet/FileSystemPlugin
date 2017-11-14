using Plugin.FileSystem.Abstractions;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Plugin.FileSystem.Test
{
    public abstract class TestBase
    {
        protected readonly IFileSystem FileSystem;

        protected TestBase(IFileSystem fileSystem)
        {
            FileSystem = fileSystem;
        }

        [Fact]
        public async Task FileOperationsWork()
        {
            var testBuffer = new byte[128];
            var random = new Random();
            random.NextBytes(testBuffer);

            var folderOne = await FileSystem.LocalStorage.CreateDirectoryAsync("One");
            var folderTwo = await FileSystem.LocalStorage.CreateDirectoryAsync("Two");

            var items = await FileSystem.LocalStorage.EnumerateFileItemsAsync();
            Assert.Collection(items, d => Assert.Equal(folderOne.FullName, d.FullName),
                d => Assert.Equal(folderTwo.FullName, d.FullName));

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
