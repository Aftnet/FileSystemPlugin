using Plugin.Filesystem.Abstractions;

namespace Plugin.FileSystem
{
    public static class CrossFileSystem
    {
        private static FileSystem current = new FileSystem();
        public static IFileSystem Current => current;
    }
}
