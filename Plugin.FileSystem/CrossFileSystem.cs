using Plugin.FileSystem.Abstractions;

namespace Plugin.FileSystem
{
    public static class CrossFileSystem
    {
        private static Lazy<FileSystem> fileSystem = new Lazy<FileSystem>(LazyThreadSafetyMode.PublicationOnly);

        public static IFileSystem Current => fileSystem.Value;
    }
}
