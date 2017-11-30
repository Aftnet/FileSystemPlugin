using Plugin.FileSystem.Test;

namespace Plugin.FileSystem.iOS.Test
{
    public class Test : TestBase
    {
        public Test() : base(CrossFileSystem.Current)
        {
        }
    }
}
