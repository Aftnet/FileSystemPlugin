using Plugin.FileSystem.Abstractions;
using System;
using System.Threading;

namespace Plugin.FileSystem
{
    public static class CrossFileSystem
    {
#if !NETSTANDARD1_3
        private static Lazy<FileSystem> fileSystem = new Lazy<FileSystem>(LazyThreadSafetyMode.PublicationOnly);
#endif

        public static bool Supported
        {
            get
            {
#if NETSTANDARD1_3
                return false;
#else
                return true;
#endif
            }
        }

        public static IFileSystem Current
        {
            get
            {
#if NETSTANDARD1_3
                throw new NotImplementedException();
#else
                return fileSystem.Value;
#endif
            }
        }
    }
}
