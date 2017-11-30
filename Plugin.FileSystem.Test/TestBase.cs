using Plugin.FileSystem.Abstractions;
using System;
using System.Linq;
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
            var fileOne = await TestRootFolder.CreateFileAsync("one.ext");
            var fileTwo = await fileOne.CopyToAsync(TestRootFolder, "two.ext");

            var files = await TestRootFolder.EnumerateFilesAsync();
            Assert.Collection(files, d => Assert.Equal(fileOne, d), d => Assert.Equal(fileTwo, d));

            var newFolder = await TestRootFolder.CreateDirectoryAsync("folder");
            fileTwo = await fileOne.CopyToAsync(newFolder);

            files = await newFolder.EnumerateFilesAsync();
            Assert.Collection(files, d => Assert.Equal(fileTwo, d));

            files = await TestRootFolder.EnumerateFilesAsync();
            Assert.Contains(files, d => d.Name == fileOne.Name);
        }

        [Fact]
        public async Task MoveFileWorks()
        {
            var fileOne = await TestRootFolder.CreateFileAsync("one.ext");
            await fileOne.MoveToAsync(TestRootFolder, "two.ext");

            var files = await TestRootFolder.EnumerateFilesAsync();
            Assert.Collection(files, d => Assert.Equal(fileOne, d));

            var newFolder = await TestRootFolder.CreateDirectoryAsync("folder");
            await fileOne.MoveToAsync(newFolder);

            files = await newFolder.EnumerateFilesAsync();
            Assert.Collection(files, d => Assert.Equal(fileOne, d));

            files = await TestRootFolder.EnumerateFilesAsync();
            Assert.Empty(files);
        }

        [Fact]
        public async Task OpenFileWorks()
        {
            var data = new byte[128];
            var random = new Random();
            random.NextBytes(data);

            var fileOne = await TestRootFolder.CreateFileAsync("one.ext");

            using (var stream = await fileOne.OpenAsync(System.IO.FileAccess.ReadWrite))
            {
                Assert.NotNull(stream);
                await stream.WriteAsync(data, 0, data.Length);
            }

            using (var stream = await fileOne.OpenAsync(System.IO.FileAccess.ReadWrite))
            {
                Assert.NotNull(stream);

                var comparison = new byte[data.Length];
                await stream.ReadAsync(comparison, 0, comparison.Length);
                Assert.Equal(data, comparison);
            }
        }

        [Fact]
        public async Task InstallLocationWorks()
        {
            var installLoaction = FileSystem.InstallLocation;
            Assert.NotNull(installLoaction);

            var files = await installLoaction.EnumerateFilesAsync();
            var bundledFile = files.FirstOrDefault(d => d.Name == "TestBundledFile.txt");
            Assert.NotNull(bundledFile);

            using (var stream = await bundledFile.OpenAsync(System.IO.FileAccess.Read))
            {
                Assert.True(stream.Length > 0);
            }
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
