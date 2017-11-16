using Plugin.FileSystem.Test;

namespace Plugin.FileSystem.Net46.Test
{
    public class Test : TestBase
    {
        public Test() : base(CrossFileSystem.Current)
        {
        }
    }
}
