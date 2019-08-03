using Plugin.FileSystem.Test;

namespace Plugin.FileSystem.UWP.Test
{
    public class Test : TestBase
    {
        public Test() : base(CrossFileSystem.Current)
        {
        }
    }
}
